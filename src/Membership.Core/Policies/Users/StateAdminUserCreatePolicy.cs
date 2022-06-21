using Membership.Core.ValueObjects;

namespace Membership.Core.Policies.Users;

internal sealed class StateAdminUserCreatePolicy : IUserCreatePolicy
{
   
    public bool CanBeApplied(UserRole currentUserRole)
        => currentUserRole == UserRole.StateAdmin();

    public IEnumerable<UserRole> PermittedUserRole(UserRole currentUserRole)
    {
        return new List<UserRole>
        {
            UserRole.DistrictAdmin()
        };
    }

    public Guid? GetStateId(Guid? cascadeId, Guid? parentId)
    {
        return cascadeId;
    }
}