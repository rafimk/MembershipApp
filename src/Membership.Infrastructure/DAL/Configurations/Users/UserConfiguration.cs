using Membership.Core.Entities.Nationalities;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Membership.Infrastructure.DAL.Configurations.Users;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new GenericId(x));
        builder.Property(x => x.FullName)
            .HasConversion(x => x.Value, x => new FullName(x))
            .IsRequired()
            .HasMaxLength(100);
        builder.HasIndex(x => x.Email).IsUnique();
        builder.Property(x => x.Email)
            .HasConversion(x => x.Value, x => new Email(x))
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(x => x.MobileNumber)
            .HasConversion(x => x.Value, x => new MobileNumber(x))
            .IsRequired()
            .HasMaxLength(20);
        builder.Property(x => x.AlternativeContactNumber)
            .HasConversion(x => x.Value, x => new MobileNumber(x))
            .IsRequired()
            .HasMaxLength(20);
        builder.Property(x => x.Role)
            .HasConversion(x => x.Value, x => new UserRole(x));
        builder.Property(x => x.CreatedAt).IsRequired();
    }
}