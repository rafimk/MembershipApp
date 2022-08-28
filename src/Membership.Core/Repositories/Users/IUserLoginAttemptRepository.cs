using Membership.Core.Entities.Users;

namespace Membership.Core.Repositories.Users;

public interface IUserLoginAttemptRepository
{
    Task<UserLoginAttempt> GetByIdAsync(Guid id);
    Task AddAsync(UserLoginAttempt userLoginAttempt);
    Task UpdateAsync(UserLoginAttempt userLoginAttempt);
    Task DeleteIdAsync(Guid id);
}