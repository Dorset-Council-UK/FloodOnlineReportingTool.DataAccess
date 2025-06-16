using FloodOnlineReportingTool.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FloodOnlineReportingTool.DataAccess.EntitiesConfiguration;

internal class RecordStatusConfiguration : IEntityTypeConfiguration<RecordStatus>
{
    public void Configure(EntityTypeBuilder<RecordStatus> builder)
    {
        builder
            .Property(o => o.Id)
            .ValueGeneratedNever();

        builder
            .ToTable(o => o.HasComment("Status used in various places including flood reports."));

        builder
            .HasData(InitialData.RecordStatusData());

        builder
            .HasIndex(o => o.Category);

        builder
            .Property(o => o.Category)
            .HasMaxLength(100);

        builder
            .Property(o => o.Text)
            .HasMaxLength(100);

        builder
            .Property(o => o.Description)
            .HasMaxLength(200);
    }
}
