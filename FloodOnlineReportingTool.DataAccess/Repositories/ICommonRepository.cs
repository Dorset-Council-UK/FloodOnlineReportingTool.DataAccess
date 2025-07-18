﻿using FloodOnlineReportingTool.DataAccess.Models;

namespace FloodOnlineReportingTool.DataAccess.Repositories;

public interface ICommonRepository
{
    Task<FloodImpact?> GetFloodImpact(Guid id, CancellationToken ct);
    Task<IList<FloodImpact>> GetFloodImpactsByCategory(string category, CancellationToken ct);
    Task<IList<FloodProblem>> GetFloodProblemsByCategory(string category, CancellationToken ct);
    Task<IList<FloodProblem>> GetFloodProblemsByCategories(string[] categories, CancellationToken ct);
    Task<IList<FloodMitigation>> GetFloodMitigationsByCategory(string category, CancellationToken ct);
    Task<IList<FloodMitigation>> GetFloodMitigationsByCategories(string[] categories, CancellationToken ct);
    Task<RecordStatus?> GetRecordStatus(Guid id, CancellationToken ct);
    Task<IList<RecordStatus>> GetRecordStatusesByCategory(string category, CancellationToken ct);
    Task<IList<RecordStatus>> GetRecordStatusesByCategories(string[] categories, CancellationToken ct);
    Task<IList<Organisation>> GetResponsibleOrganisations(double? easting, double? northing, CancellationToken ct);
}
