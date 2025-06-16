using FloodOnlineReportingTool.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FloodOnlineReportingTool.DataAccess.EntitiesConfiguration;

internal class FloodImpactConfiguration : IEntityTypeConfiguration<FloodImpact>
{
    public void Configure(EntityTypeBuilder<FloodImpact> builder)
    {
        builder
            .Property(o => o.Id)
            .ValueGeneratedNever();

        builder
            .ToTable(o => o.HasComment("Represents the broader impacts of a flood, such as health risks, economic losses, etc"));

        builder
            .HasData(InitialData.FloodImpactData());

        builder
            .HasIndex(o => o.Category);
    }
}
