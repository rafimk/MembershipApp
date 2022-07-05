using Membership.Core.Entities.Memberships.Members;
using Membership.Core.Repositories.Memberships;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Repositories.Memberships;

internal sealed class PostgresDisputeRequestRepository : IDisputeRequestRepository
{
    private readonly MembershipDbContext _dbContext;

    public PostgresDisputeRequestRepository(MembershipDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public  Task<Member> GetByIdAsync(GenericId id)
        => _dbContext.Members.Include(x => x.Area)
                .Include(x => x.Mandalam).SingleOrDefaultAsync(x => x.Id == id);

    public  Task<Member> GetByMemberIdAsync(MembershipId membershipId)
        => _dbContext.Members.Include(x => x.Area)
            .Include(x => x.Mandalam).SingleOrDefaultAsync(x => x.MembershipId == membershipId);

    public  Task<Member> GetByEmiratesIdAsync(EmiratesIdNumber emiratesId)
        => _dbContext.Members.Include(x => x.Area)
            .Include(x => x.Mandalam).SingleOrDefaultAsync(x => x.EmiratesIdNumber == emiratesId);
    
    public  Task<Member> GetByEmailAsync(Email email)
        => _dbContext.Members.Include(x => x.Area)
            .Include(x => x.Mandalam).SingleOrDefaultAsync(x => x.Email == email);
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

    public string GenerateMembershipId(string prefix)
    {
        var totalRecordCount = _dbContext.Members.Count() + 1;
        return $"{prefix.Trim()}{totalRecordCount.ToString("D6")}";
    }
}