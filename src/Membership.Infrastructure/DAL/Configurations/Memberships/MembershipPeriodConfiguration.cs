using Membership.Core.Entities.Memberships.MembershipPeriods;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Membership.Infrastructure.DAL.Configurations.Memberships;

internal sealed class MembershipPeriodConfiguration : IEntityTypeConfiguration<MembershipPeriod>
{
    public void Configure(EntityTypeBuilder<MembershipPeriod> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Start)
            .HasConversion(x => x.Value, x => new Date(x))
            .IsRequired();
        builder.Property(x => x.End)
            .HasConversion(x => x.Value, x => new Date(x))
            .IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
    }
}