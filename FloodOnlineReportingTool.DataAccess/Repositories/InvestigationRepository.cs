﻿using FloodOnlineReportingTool.DataAccess.DbContexts;
using FloodOnlineReportingTool.DataAccess.Models;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace FloodOnlineReportingTool.DataAccess.Repositories;

public class InvestigationRepository(
    FORTDbContext context,
    IPublishEndpoint publishEndpoint
) : IInvestigationRepository
{
    [Authorize]
    public async Task<Investigation?> ReportedByUser(Guid userId, Guid id, CancellationToken ct)
    {
        return await context.FloodReports
            .AsNoTracking()
            .Include(o => o.Investigation)
            .Where(o => o.ReportedByUserId == userId)
            .Select(o => o.Investigation)
            .FirstOrDefaultAsync(o => o != null && o.Id == id, ct)
            .ConfigureAwait(false);
    }

    [Authorize]
    public async Task<Investigation> CreateForUser(Guid userId, InvestigationDto investigationDto, CancellationToken ct)
    {
        var floodReport = await context.FloodReports
            .AsNoTracking()
            .Include(o => o.EligibilityCheck)
            .FirstOrDefaultAsync(o => o.ReportedByUserId == userId, ct)
            .ConfigureAwait(false);

        if (floodReport == null)
        {
            throw new InvalidOperationException("No flood report found");
        }
        if (floodReport.InvestigationId != null)
        {
            throw new InvalidOperationException("An investigation already exists for this flood report");
        }
        if (floodReport.StatusId != RecordStatusIds.ActionNeeded)
        {
            throw new InvalidOperationException("There is not currently an ongoing investigation for this flood report");
        }

        var isInternal = floodReport.EligibilityCheck?.IsInternal() ?? false;
        var investigation = investigationDto.ToInvestigation(isInternal) with
        {
            CreatedUtc = DateTimeOffset.UtcNow,
        };

        var updatedFloodReport = floodReport with
        {
            InvestigationId = investigation.Id,
            StatusId = RecordStatusIds.ActionCompleted,
        };

        context.Investigations.Add(investigation);
        context.FloodReports.Update(updatedFloodReport);

        // Publish a message to the message system
        var message = investigation.ToMessageCreated(floodReport.Reference);
        await publishEndpoint
            .Publish(message, ct)
            .ConfigureAwait(false);

        // Save the investigation in the database
        await context
            .SaveChangesAsync(ct)
            .ConfigureAwait(false);

        return investigation;
    }

    [Authorize]
    public async Task<Investigation?> ReportedByUserBasicInformation(Guid userId, CancellationToken ct)
    {
        return await context.FloodReports
            .AsNoTracking()
            .IgnoreAutoIncludes()
            .Include(o => o.Investigation)
            .Where(o => o.ReportedByUserId == userId)
            .Select(o => o.Investigation)
            .FirstOrDefaultAsync(ct)
            .ConfigureAwait(false);
    }
}
