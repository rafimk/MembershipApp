using Membership.Core.Entities.Memberships.Members;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Membership.Infrastructure.DAL.Configurations.Memberships;

internal sealed class MembershipVerificationConfiguration : IEntityTypeConfiguration<MembershipVerification>
{
    public void Configure(EntityTypeBuilder<MembershipVerification> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.MemberId).IsRequired();
        builder.HasIndex(u => u.MemberId).IsUnique();
        builder.HasOne(x => x.Member);
        builder.Property(x => x.Gender).HasConversion<string>();
        builder.Property(x => x.CardType).HasConversion<string>();
    }
}