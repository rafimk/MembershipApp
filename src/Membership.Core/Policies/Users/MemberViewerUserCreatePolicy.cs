using Membership.Core.ValueObjects;

namespace Membership.Core.Policies.Users;

internal sealed class GovVerificationOfficerUserCreatePolicy : IUserCreatePolicy
{
    public bool CanBeApplied(UserRole currentUserRole)
        => currentUserRole == UserRole.MemberViewer();

    public IEnumerable<UserRole> PermittedUserRole(UserRole currentUserRole)
    {
        return new List<UserRole>();
    }

    public Guid? GetStateId(Guid? cascadeId, Guid? parentId)
    {
        return null;
    }
}