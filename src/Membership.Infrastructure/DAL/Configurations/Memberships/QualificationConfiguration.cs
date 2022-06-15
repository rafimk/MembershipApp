using Membership.Core.Entities.Memberships;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Membership.Infrastructure.DAL.Configurations.Memberships;

internal sealed class QualificationConfiguration : IEntityTypeConfiguration<Qualification>
{
    public void Configure(EntityTypeBuilder<Qualification> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new GenericId(x));
        builder.Property(x => x.Name)
            .HasConversion(x => x.Value, x => new QualificationName(x))
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(x => x.CreatedAt).IsRequired();
    }
}