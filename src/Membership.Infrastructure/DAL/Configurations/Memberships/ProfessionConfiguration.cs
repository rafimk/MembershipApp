using Membership.Core.Entities.Memberships.Professions;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Membership.Infrastructure.DAL.Configurations.Memberships;

internal sealed class ProfessionConfiguration : IEntityTypeConfiguration<Profession>
{
    public void Configure(EntityTypeBuilder<Profession> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new GenericId(x));
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(x => x.CreatedAt).IsRequired();
    }
}