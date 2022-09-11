using System.Collections;
using Membership.Core.ValueObjects;

namespace Membership.Core.Policies.Users;

internal sealed class CentralCommitteeUserCreatePolicy : IUserCreatePolicy
{
    public bool CanBeApplied(UserRole currentUserRole)
        => currentUserRole == UserRole.CentralCommitteeAdmin();

    public IEnumerable<UserRole> PermittedUserRole(UserRole currentUserRole)
    {
        return new List<UserRole>
        {
            UserRole.CentralCommitteeAdmin(),
            UserRole.StateAdmin(),
            UserRole.DisputeCommittee(),
            UserRole.MonitoringOfficer(),
            UserRole.VerificationOfficer(),
        };
    }

    public Guid? GetStateId(Guid? cascadeId, Guid? parentId)
    {
        return cascadeId;
    }
}