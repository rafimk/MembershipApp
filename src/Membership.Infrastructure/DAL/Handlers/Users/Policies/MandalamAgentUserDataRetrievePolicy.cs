using Membership.Application.DTO.Users;
using Membership.Core.ValueObjects;

namespace Membership.Infrastructure.DAL.Handlers.Users.Policies;

internal sealed class MandalamAgentUserDataRetrievePolicy : IUserDataRetrievePolicy
{
    public bool CanBeApplied(UserRole currentUserRole)
        => currentUserRole == UserRole.MandalamAgent();

    public IEnumerable<UserDto> GetData(UserRole currentUserRole, List<UserDto> availableUsers, 
        IEnumerable<string> applicableUserRoles, Guid? stateId, Guid? districtId)
    {
        return null;
    }
}