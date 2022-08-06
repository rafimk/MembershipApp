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

        builder.Property(x => x.SubmittedDate).IsRequired();
        builder.Property(x => x.Status).HasConversion<string>();
    }
}