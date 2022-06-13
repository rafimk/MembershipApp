using Membership.Core.ValueObjects;

namespace Membership.Core.Policies.Users;

internal sealed class CentralCommitteeUserCreatePolicy : IUserCreatePolicy
{
    public bool CanBeApplied(UserRole currentUserRole)
        => currentUserRole == UserRole.CentralCommitteeAdmin;

    public UserRole PermittedUserRole(UserRole currentUserRole)
    {
        return UserRole.StateAdmin;
    }
}