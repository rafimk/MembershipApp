using Membership.Core.Consts;
using Membership.Core.Entities.Memberships.Disputes;
using Membership.Core.Repositories.Memberships;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Repositories.Memberships;

internal sealed class PostgresDisputeRequestRepository : IDisputeRequestRepository
{
    private readonly MembershipDbContext _dbContext;

    public PostgresDisputeRequestRepository(MembershipDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DisputeRequest> GetByIdAsync(Guid id)
        => await _dbContext.DisputeRequests
            .Include(x => x.ToState)
            .Include(x => x.ToArea)
            .Include(x => x.ToMandalam)
            .Include(x => x.ToPanchayat)
            .Include(x => x.FromState)
            .Include(x => x.FromArea)
            .Include(x => x.FromMandalam)
            .Include(x => x.FromPanchayat)
            .SingleOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<DisputeRequest>> GetByMemberIdAsync(Guid memberId)
        => await _dbContext.DisputeRequests
            .Include(x => x.ToState)
            .Include(x => x.ToArea)
            .Include(x => x.ToMandalam)
            .Include(x => x.ToPanchayat)
            .Include(x => x.FromState)
            .Include(x => x.FromArea)
            .Include(x => x.FromMandalam)
            .Include(x => x.FromPanchayat)
            .Where(x => x.MemberId == memberId)
            .ToListAsync();
    
    public async Task<IEnumerable<DisputeRequest>> GetPendingByMemberIdAsync(Guid memberId)
        => await _dbContext.DisputeRequests
            .Include(x => x.ToState)
            .Include(x => x.ToArea)
            .Include(x => x.ToMandalam)
            .Include(x => x.ToPanchayat)
            .Include(x => x.FromState)
            .Include(x => x.FromArea)
            .Include(x => x.FromMandalam)
            .Include(x => x.FromPanchayat)
            .Where(x => x.MemberId == memberId && 
                   x.Status == DisputeStatus.Pending)
            .ToListAsync();

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