using Membership.Core.Entities.Users;
using Membership.Core.ValueObjects;

namespace Membership.Core.DomainServices.Users;

public interface IUserService
{
    Task<IEnumerable<string>> GetApplicableUserRolesAsync(UserRole role, string userId);
    Guid? GetStateId(Guid? cascadeId, Guid? parentUserId);
}


