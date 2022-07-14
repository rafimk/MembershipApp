using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.Disputes;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.Disputes;

internal sealed class GetDisputeRequestsHandler : IQueryHandler<GetDisputeRequests, IEnumerable<DisputeRequestDto>>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetDisputeRequestsHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<IEnumerable<DisputeRequestDto>> HandleAsync(GetDisputeRequests query)
    {
        var disputeRequests = await _dbContext.DisputeRequests
            .Include(x => x.Member).ThenInclude(x => x.Area)
            .Include(x => x.ProposedArea).ThenInclude(x => x.State)
            .Include(x => x.ProposedMandalam).ThenInclude(x => x.District)
            .Include(x => x.ProposedPanchayat).ThenInclude(x => x.Mandalam)
            .AsNoTracking()
            .Select(x => x.AsDto())
            .ToListAsync();

        return disputeRequests;
    }
}