using Membership.Core.Entities.Memberships.Members;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Membership.Infrastructure.DAL.Configurations.Memberships;

internal sealed class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.MembershipId)
            .HasConversion(x => x.Value, x => new MembershipId(x))
            .IsRequired()
            .HasMaxLength(15);
        
        builder.Property(x => x.FullName)
            .HasConversion(x => x.Value, x => new FullName(x))
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(x => x.EmiratesIdNumber)
            .HasConversion(x => x.Value, x => new EmiratesIdNumber(x))
            .IsRequired()
            .HasMaxLength(25);
        
        builder.Property(x => x.EmiratesIdExpiry)
            .HasConversion(x => x.Value, x => new Date(x))
            .IsRequired();
        
        builder.Property(x => x.DateOfBirth)
            .HasConversion(x => x.Value, x => new Date(x))
            .IsRequired();
        
        builder.Property(x => x.MobileNumber)
            .HasConversion(x => x.Value, x => new MobileNumber(x))
            .IsRequired()
            .HasMaxLength(20);
        
        builder.Property(x => x.Email)
            .HasMaxLength(100);
        
        builder.Property(x => x.PassportNumber)
            .HasMaxLength(25);
        
        builder.Property(x => x.Gender).HasConversion<string>();
        builder.Property(x => x.BloodGroup).HasConversion<string>();
        builder.Property(x => x.Status).HasConversion<string>();
        builder.Property(x => x.CreatedAt).IsRequired();
 }
}