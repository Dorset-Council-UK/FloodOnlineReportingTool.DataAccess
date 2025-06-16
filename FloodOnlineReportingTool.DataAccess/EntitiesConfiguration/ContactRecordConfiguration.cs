using FloodOnlineReportingTool.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FloodOnlineReportingTool.DataAccess.EntitiesConfiguration;

internal class ContactRecordConfiguration : IEntityTypeConfiguration<ContactRecord>
{
    public void Configure(EntityTypeBuilder<ContactRecord> builder)
    {
        builder
            .Property(o => o.Id)
            .ValueGeneratedNever();

        builder
            .ToTable(o => o.HasComment("Contact information for individuals reporting flood incidents and seeking assistance"));
    }
}
