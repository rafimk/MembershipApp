using Membership.Core.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Membership.Infrastructure.DAL.Configurations.Users;


internal sealed class UserLoginAttemptConfiguration : IEntityTypeConfiguration<UserLoginAttempt>
{
    public void Configure(EntityTypeBuilder<UserLoginAttempt> builder)
    {
    }
}