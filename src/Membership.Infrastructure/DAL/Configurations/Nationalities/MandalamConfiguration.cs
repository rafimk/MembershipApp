using Membership.Core.Entities.Nationalities;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Membership.Infrastructure.DAL.Configurations.Nationalities;

internal sealed class MandalamConfiguration : IEntityTypeConfiguration<Mandalam>
{
    public void Configure(EntityTypeBuilder<Mandalam> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name)
            .HasConversion(x => x.Value, x => new MandalamName(x))
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(x => x.CreatedAt).IsRequired();
    }
}