using Membership.Core.Entities.Memberships.RegisteredOrganizations;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Membership.Infrastructure.DAL.Configurations.Memberships;

internal sealed class RegisteredOrganizationConfiguration : IEntityTypeConfiguration<RegisteredOrganization>
{
    public void Configure(EntityTypeBuilder<RegisteredOrganization> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(x => x.CreatedAt).IsRequired();
    }
}