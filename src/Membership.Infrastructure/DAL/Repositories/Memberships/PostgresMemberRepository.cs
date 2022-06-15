using Membership.Core.Entities.Nationalities;
using Membership.Core.Repositories.Nationalities;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Repositories.Memberships;

internal sealed class PostgresMemberRepository : IMemberRepository
{
    private readonly MMSDbContext _dbContext;

    public PostgresMembershipRepository(MMSDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public  Task<Member> GetByIdAsync(GenericId id)
        => _dbContext.Members.Include(x => x.Area)
                .Include(x => x.Mandalam).SingleOrDefaultAsync(x => x.Id == id);

    public  Task<Member> GetByMemberIdAsync(MembershipId membershipId)
        => _dbContext.Members.Include(x => x.Area)
            .Include(x => x.Mandalam).SingleOrDefaultAsync(x => x.MembershipId == membershipId);
    
     public  Task<Member> GetByMemberIdAsync(EmiratesId emiratesId)
        => _dbContext.Members.Include(x => x.Area)
            .Include(x => x.Mandalam).SingleOrDefaultAsync(x => x.EmiratesId == emiratesId);
    
    public async Task<IEnumerable<Member>> GetAsync()
        => await _dbContext.Members.Where(x => x.IsActive).ToListAsync();
    
    public async Task AddAsync(Member member)
    {
        await _dbContext.Members.AddAsync(member);
    }

    public async Task UpdateAsync(Member member)
    {
        _dbContext.Members.Update(member);
        await Task.CompletedTask;
    }
}