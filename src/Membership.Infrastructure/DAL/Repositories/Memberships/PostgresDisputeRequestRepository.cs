using Membership.Core.Entities.Memberships.Disputes;
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

    public  Task<DisputeRequest> GetByIdAsync(GenericId id)
        => _dbContext.DisputeRequests.Include(x => x.ProposedArea)
                .Include(x => x.ProposedMandalam)
                .Include(x => x.ProposedPanchayat).SingleOrDefaultAsync(x => x.Id == id);

    public  Task<DisputeRequest> GetByMemberIdAsync(GenericId memberId)
        => _dbContext.DisputeRequests.Include(x => x.ProposedArea)
            .Include(x => x.ProposedMandalam)
            .Include(x => x.ProposedPanchayat).SingleOrDefaultAsync(x => x.MemberId == memberId);

    public async Task<IEnumerable<DisputeRequest>> GetAsync()
        => await _dbContext.DisputeRequests.ToListAsync();
    
    public async Task AddAsync(DisputeRequest request)
    {
        await _dbContext.DisputeRequests.AddAsync(request);
    }

    public async Task UpdateAsync(DisputeRequest request)
    {
        _dbContext.DisputeRequests.Update(request);
        await Task.CompletedTask;
    }
}