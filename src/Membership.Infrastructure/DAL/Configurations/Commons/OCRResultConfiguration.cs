using Membership.Core.Entities.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Membership.Infrastructure.DAL.Configurations.Commons;

internal sealed class OCRResultConfiguration : IEntityTypeConfiguration<OCRResult>
{
    public void Configure(EntityTypeBuilder<OCRResult> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Type).HasConversion<string>();
    }
}