using Membership.Core.Entities.Nationalities;
using Membership.Core.Repositories.Nationalities;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Repositories.Users;

internal sealed class PostgresUserRepository : IUserRepository
{
    private readonly MMSDbContext _dbContext;

    public PostgresUserRepository(MMSDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public  Task<User> GetByIdAsync(GenericId id)
        => _dbContext.Users.SingleOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<User>> GetAsync()
        => await _dbContext.Users.Where(x => x.IsActive).ToListAsync();
    
    public  Task<User> GetByEmailAsync(Email email)
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