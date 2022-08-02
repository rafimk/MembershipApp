using Membership.Application.DTO.Users;
using Membership.Core.ValueObjects;

namespace Membership.Infrastructure.DAL.Handlers.Users.Policies;

internal sealed class DistrictAdminUserDataRetrievePolicy : IUserDataRetrievePolicy
{
    public bool CanBeApplied(UserRole currentUserRole)
        => currentUserRole == UserRole.DistrictAdmin();

    public IEnumerable<UserDto> GetData(UserRole currentUserRole, List<UserDto> availableUsers, 
        IEnumerable<string> applicableUserRoles, Guid? stateId, Guid? districtId)
    {
        return availableUsers.Where(x => applicableUserRoles.Contains(x.Role) && 
                                         x.StateId == stateId && x.DistrictId == districtId).ToList();
    }
}