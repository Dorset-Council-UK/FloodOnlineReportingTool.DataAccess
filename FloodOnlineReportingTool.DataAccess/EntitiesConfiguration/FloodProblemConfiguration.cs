﻿using FloodOnlineReportingTool.DataAccess.Models;
using FloodOnlineReportingTool.DataAccess.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FloodOnlineReportingTool.DataAccess.EntitiesConfiguration;

internal class FloodProblemConfiguration : IEntityTypeConfiguration<FloodProblem>
{
    public void Configure(EntityTypeBuilder<FloodProblem> builder)
    {
        builder
            .Property(o => o.Id)
            .ValueGeneratedNever();

        builder
            .ToTable(o => o.HasComment("Flood problems related to the occurrence, cause, or characteristics of a flood."));

        builder
            .HasData(InitialData.FloodProblemData());

        builder
            .HasIndex(o => o.Category);
    }
}
