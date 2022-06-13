using Membership.Core.ValueObjects;

namespace Membership.Core.Policies.Users;

internal sealed class DistrictAdminUserCreatePolicy : IUserCreatePolicy
{
    public bool CanBeApplied(UserRole currentUserRole)
        => currentUserRole == UserRole.DistrictAdmin;

    public UserRole PermittedUserRole(UserRole currentUserRole)
    {
        return UserRole.MandalamAgent;
    }
}