using Membership.Application.DTO.Users;
using Membership.Core.ValueObjects;

namespace Membership.Infrastructure.DAL.Handlers.Users.Policies;

public interface IUserDataRetrievePolicy
{
    bool CanBeApplied(UserRole currentUserRole);
    IEnumerable<UserDto> GetData(UserRole currentUserRole, List<UserDto> availableUsers, 
        IEnumerable<string> applicableUserRoles, Guid? stateId, Guid? districtId);
}