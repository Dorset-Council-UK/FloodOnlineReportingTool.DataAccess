using FloodOnlineReportingTool.DataAccess.Models;
using FloodOnlineReportingTool.DataAccess.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FloodOnlineReportingTool.DataAccess.EntitiesConfiguration;

internal class FloodMitigationConfiguration : IEntityTypeConfiguration<FloodMitigation>
{
    public void Configure(EntityTypeBuilder<FloodMitigation> builder)
    {
        builder
            .Property(o => o.Id)
            .ValueGeneratedNever();

        builder
            .ToTable(o => o.HasComment("Actions and measures taken to reduce or prevent the impact of flooding"));

        builder
            .HasData(InitialData.FloodMitigationData());
    }
}
