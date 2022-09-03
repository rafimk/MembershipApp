using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Membership.Infrastructure.DAL.ReadModels.Configurations;

internal sealed class MemberReadModelConfiguration : IEntityTypeConfiguration<MemberReadModel>
{
    public void Configure(EntityTypeBuilder<MemberReadModel> builder)
    {
        builder.Property(x => x.Status).HasConversion<string>();
        builder.Property(x => x.CreatedAt).IsRequired();
    }
}