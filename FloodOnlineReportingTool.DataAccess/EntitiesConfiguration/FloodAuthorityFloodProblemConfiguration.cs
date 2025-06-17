using FloodOnlineReportingTool.DataAccess.Models;
using FloodOnlineReportingTool.DataAccess.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FloodOnlineReportingTool.DataAccess.EntitiesConfiguration;

internal class FloodAuthorityFloodProblemConfiguration : IEntityTypeConfiguration<FloodAuthorityFloodProblem>
{
    public void Configure(EntityTypeBuilder<FloodAuthorityFloodProblem> builder)
    {
        builder
            .HasKey(o => new { o.FloodAuthorityId, o.FloodProblemId });

        builder
            .Property(o => o.FloodAuthorityId)
            .ValueGeneratedNever();

        builder
            .Property(o => o.FloodProblemId)
            .ValueGeneratedNever();

        builder
            .ToTable(o => o.HasComment("Relationships between flood authorities and flood problems"));

        builder
            .HasData(InitialData.FloodAuthorityFloodProblemsData());

        // Auto includes
        builder
            .Navigation(o => o.FloodProblem)
            .AutoInclude();
    }
}
