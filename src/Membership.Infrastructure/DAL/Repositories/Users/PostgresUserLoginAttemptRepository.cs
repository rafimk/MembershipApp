using Membership.Core.Entities.Users;
using Membership.Core.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Repositories.Users;

internal sealed class PostgresUserLoginAttemptRepository : IUserLoginAttemptRepository
{
    private readonly MembershipDbContext _dbContext;

    public PostgresUserLoginAttemptRepository(MembershipDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public  Task<UserLoginAttempt> GetByIdAsync(Guid id)
        => _dbContext.UserLoginAttempts.SingleOrDefaultAsync(x => x.Id == id);


    public async Task AddAsync(UserLoginAttempt userLoginAttempt)
    {
        await _dbContext.UserLoginAttempts.AddAsync(userLoginAttempt);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(UserLoginAttempt userLoginAttempt)
    {
        _dbContext.UserLoginAttempts.Update(userLoginAttempt);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task DeleteIdAsync(Guid id)
    {
        var userLoginAttempt = await _dbContext.UserLoginAttempts.SingleOrDefaultAsync(x => x.Id == id);
        if (userLoginAttempt is null)
        {
            return;
        }
        _dbContext.UserLoginAttempts.Remove(userLoginAttempt);
        await _dbContext.SaveChangesAsync();
    }
}