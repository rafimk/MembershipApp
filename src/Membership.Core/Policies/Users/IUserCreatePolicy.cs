using Membership.Core.ValueObjects;

namespace Membership.Core.Policies.Users;

public interface IUserCreatePolicy
{
    bool CanBeApplied(UserRole currentUserRole);
    IEnumerable<UserRole> PermittedUserRole(UserRole currentUserRole);
}