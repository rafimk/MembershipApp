using Membership.Core.Entities.Users;
using Membership.Core.ValueObjects;

namespace Membership.Core.Repositories.Users;

public interface IUserRepository
{
    Task<User> GetByIdAsync(Guid id);
    Task<User> GetByEmailAsync(Email email);
    Task<User> GetByMobileAsync(MobileNumber mobileNumber);
    Task<IEnumerable<User>> GetAsync();
    Task AddAsync(User user);
    Task UpdateAsync(User user);
}