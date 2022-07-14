using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.Disputes;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.Disputes;

internal sealed class GetDisputeRequestsByAreaIdHandler : IQueryHandler<GetDisputeRequestsByAreaId, IEnumerable<DisputeRequestDto>>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetDisputeRequestsByAreaIdHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<IEnumerable<DisputeRequestDto>> HandleAsync(GetDisputeRequestsByAreaId query)
    {
        var areaId = new GenericId(query.AreaId);
        var disputeRequests = await _dbContext.DisputeRequests
            .Include(x => x.ProposedArea).ThenInclude(x => x.State)
            .Include(x => x.ProposedMandalam).ThenInclude(x => x.District)
            .Include(x => x.ProposedPanchayat).ThenInclude(x => x.Mandalam)
            .AsNoTracking()
            .Where(x => x.ProposedAreaId == areaId)
            .Select(x => x.AsDto())
            .ToListAsync();

        return disputeRequests;
    }
}