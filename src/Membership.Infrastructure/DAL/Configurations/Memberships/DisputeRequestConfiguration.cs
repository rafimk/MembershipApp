using Membership.Core.Entities.Memberships.Disputes;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Membership.Infrastructure.DAL.Configurations.Memberships;

internal sealed class DisputeRequestConfiguration : IEntityTypeConfiguration<DisputeRequest>
{
    public void Configure(EntityTypeBuilder<DisputeRequest> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new GenericId(x));
        builder.Property(x => x.MemberId)
            .HasConversion(x => x.Value, x => new GenericId(x))
            .IsRequired();
        builder.Property(x => x.ProposedAreaId)
            .HasConversion(x => x.Value, x => new GenericId(x))
            .IsRequired();
        builder.Property(x => x.ProposedMandalamId)
            .HasConversion(x => x.Value, x => new GenericId(x))
            .IsRequired();
        builder.Property(x => x.ProposedPanchayatId)
            .HasConversion(x => x.Value, x => new GenericId(x))
            .IsRequired();
        builder.Property(x => x.SubmittedBy)
            .HasConversion(x => x.Value, x => new GenericId(x))
            .IsRequired();
        builder.Property(x => x.SubmittedDate).IsRequired();
    }
}