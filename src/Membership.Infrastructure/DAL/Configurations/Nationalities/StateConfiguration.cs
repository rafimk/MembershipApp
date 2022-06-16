using Membership.Core.Entities.Nationalities;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Membership.Infrastructure.DAL.Configurations.Nationalities;

internal sealed class StateConfiguration : IEntityTypeConfiguration<State>
{
    public void Configure(EntityTypeBuilder<State> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new GenericId(x));
        builder.Property(x => x.Name)
            .HasConversion(x => x.Value, x => new StateName(x))
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(x => x.Prefix)
            .HasMaxLength(5);
        builder.Property(x => x.CreatedAt).IsRequired();
    }
}