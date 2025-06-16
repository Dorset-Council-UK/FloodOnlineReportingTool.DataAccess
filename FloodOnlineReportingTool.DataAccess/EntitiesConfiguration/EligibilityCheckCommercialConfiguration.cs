using FloodOnlineReportingTool.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FloodOnlineReportingTool.DataAccess.EntitiesConfiguration;

internal class EligibilityCheckCommercialConfiguration : IEntityTypeConfiguration<EligibilityCheckCommercial>
{
    public void Configure(EntityTypeBuilder<EligibilityCheckCommercial> builder)
    {
        builder
            .HasKey(o => new { o.EligibilityCheckId, o.FloodImpactId });

        builder
            .Property(o => o.EligibilityCheckId)
            .ValueGeneratedNever();

        builder
            .Property(o => o.FloodImpactId)
            .ValueGeneratedNever();

        builder
            .ToTable(o => o.HasComment("Relationships between eligibility checks and commercial flood impacts"));

        // Auto includes
        builder
            .Navigation(o => o.FloodImpact)
            .AutoInclude();
    }
}
