using Membership.Core.ValueObjects;

namespace Membership.Core.DomainServices.Users;

public interface IUserService
{
    Task<IEnumerable<string>> GetApplicableUserRolesAsync(UserRole role, Guid? userId);
    Task<Guid?> GetStateId(Guid? cascadeId, Guid? parentUserId);
}


