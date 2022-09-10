using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Membership.Infrastructure.DAL.ReadModels.Configurations;

internal sealed class DisputeRequestReadModelConfiguration : IEntityTypeConfiguration<DisputeRequestReadModel>
{
    public void Configure(EntityTypeBuilder<DisputeRequestReadModel> builder)
    {
        builder.Property(x => x.Status).HasConversion<string>();
    }
}