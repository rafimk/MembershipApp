using Membership.Core.Entities.Users;
using Membership.Core.Repositories.Users;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Repositories.Users;

internal sealed class PostgresUserRepository : IUserRepository
{
    private readonly MembershipDbContext _dbContext;

    public PostgresUserRepository(MembershipDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public  Task<User> GetByIdAsync(Guid id)
        => _dbContext.Users.SingleOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<User>> GetAsync()
        => await _dbContext.Users.Where(x => x.IsActive).ToListAsync();
    
    public  Task<User> GetByEmailAsync(string email)
        => _dbContext.Users.SingleOrDefaultAsync(x => x.Email == email);
    
    public  Task<User> GetByMobileAsync(MobileNumber mobileNumber)
        => _dbContext.Users.SingleOrDefaultAsync(x => x.MobileNumber == mobileNumber);

    public async Task AddAsync(User users)
    {
        await _dbContext.Users.AddAsync(users);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(User users)
    {
        _dbContext.Users.Update(users);
        await _dbContext.SaveChangesAsync();
    }
}