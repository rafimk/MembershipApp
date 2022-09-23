using Membership.Core.Entities.Memberships.Members;
using Membership.Core.Repositories.Memberships;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Repositories.Memberships;

internal sealed class PostgresMembershipVerificationRepository : IMembershipVerificationRepository
{
    private readonly MembershipDbContext _dbContext;

    public PostgresMembershipVerificationRepository(MembershipDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<MembershipVerification> GetByIdAsync(Guid id)
        => _dbContext.MembershipVerifications.Include(x => x.Member)
            .Include(x => x.VerifiedUser)
            .SingleOrDefaultAsync(x => x.Id == id);

    public Task<MembershipVerification> GetByMemberIdAsync(Guid memberId) 
        => _dbContext.MembershipVerifications.Include(x => x.Member)
        .Include(x => x.VerifiedUser)
        .SingleOrDefaultAsync(x => x.MemberId == memberId);

    public async Task<IEnumerable<MembershipVerification>> GetAsync()  
        => await _dbContext.MembershipVerifications.Include(x => x.Member)
            .Include(x => x.VerifiedUser).ToListAsync();

    public async Task AddAsync(MembershipVerification membershipVerification)
    {
        await _dbContext.MembershipVerifications.AddAsync(membershipVerification);
    }

    public async Task UpdateAsync(MembershipVerification membershipVerification)
    {
        _dbContext.MembershipVerifications.Update(membershipVerification);
        await Task.CompletedTask;
    }
}