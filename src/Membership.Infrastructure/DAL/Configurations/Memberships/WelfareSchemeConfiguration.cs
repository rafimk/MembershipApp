using Membership.Core.Entities.Memberships.RegisteredOrganizations;
using Membership.Core.Entities.Memberships.WelfareSchemes;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Membership.Infrastructure.DAL.Configurations.Memberships;

internal sealed class WelfareSchemeConfiguration : IEntityTypeConfiguration<WelfareScheme>
{
    public void Configure(EntityTypeBuilder<WelfareScheme> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new GenericId(x));
        builder.Property(x => x.Name)
            .HasConversion(x => x.Value, x => new WelfareSchemeName(x))
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(x => x.CreatedAt).IsRequired();
    }
}