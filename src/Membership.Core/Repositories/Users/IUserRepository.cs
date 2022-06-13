using Membership.Core.Entities.Users;
using Membership.Core.ValueObjects;

namespace Membership.Core.Repositories.Users;

public interface IUserRepository
{
    Task<User> GetByIdAsync(GenericId id);
    Task<User> GetByEmailAsync(Email email);
    Task<User> GetByMobileAsync(MobileNumber mobileNumber);
    Task<IEnumerable<User>> GetAsync();
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task ChangePassword(GenericId id, string newPasswordHash);
    Task Activate(GenericId id);
    Task Deactivate(GenericId id);
    Task Verify(GenericId id, DateTime verifiedAt);
}