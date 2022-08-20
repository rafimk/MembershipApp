using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.Disputes;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.Disputes;

internal sealed class GetDisputeRequestsByStateIdHandler : IQueryHandler<GetDisputeRequestsByStateId, IEnumerable<DisputeRequestDto>>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetDisputeRequestsByStateIdHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<IEnumerable<DisputeRequestDto>> HandleAsync(GetDisputeRequestsByStateId query)
    {
        var stateId = query.StateId;
        var disputeRequests = await _dbContext.DisputeRequests
            .Include(x => x.Member)
            .Include(x => x.FromState)
            .Include(x => x.FromArea)
            .Include(x => x.FromDistrict)
            .Include(x => x.FromMandalam)
            .Include(x => x.FromPanchayat)
            .Include(x => x.ToState)
            .Include(x => x.ToArea)
            .Include(x => x.ToDistrict)
            .Include(x => x.ToMandalam)
            .Include(x => x.ToPanchayat)
            .AsNoTracking()
            .Where(x => x.ToStateId == stateId)
            .Select(x => x.AsDto())
            .ToListAsync();

        return disputeRequests;
    }
}