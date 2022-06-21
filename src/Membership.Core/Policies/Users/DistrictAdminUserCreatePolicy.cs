using Membership.Core.ValueObjects;

namespace Membership.Core.Policies.Users;

internal sealed class DistrictAdminUserCreatePolicy : IUserCreatePolicy
{
    public bool CanBeApplied(UserRole currentUserRole)
        => currentUserRole == UserRole.DistrictAdmin();

    public IEnumerable<UserRole> PermittedUserRole(UserRole currentUserRole)
    {
        return new List<UserRole>
        {
            UserRole.MandalamAgent()
        };
    }

    public Guid? GetStateId(Guid? cascadeId, Guid? parentId)
    {
        return parentId;
    }
}