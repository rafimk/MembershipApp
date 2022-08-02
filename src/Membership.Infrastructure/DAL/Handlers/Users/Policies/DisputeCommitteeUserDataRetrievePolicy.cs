using Membership.Application.DTO.Users;
using Membership.Core.ValueObjects;

namespace Membership.Infrastructure.DAL.Handlers.Users.Policies;

internal sealed class DisputeCommitteeUserDataRetrievePolicy : IUserDataRetrievePolicy
{
    public bool CanBeApplied(UserRole currentUserRole)
        => currentUserRole == UserRole.DisputeCommittee();

    public IEnumerable<UserDto> GetData(UserRole currentUserRole, List<UserDto> availableUsers, 
        IEnumerable<string> applicableUserRoles, Guid? stateId, Guid? districtId)
    {
        return null;
    }
}