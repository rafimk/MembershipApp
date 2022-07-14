using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.Disputes;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.Disputes;

internal sealed class GetDisputeRequestByMandalamIdHandler : IQueryHandler<GetDisputeRequestByMandalamId, IEnumerable<DisputeRequestDto>>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetDisputeRequestByMandalamIdHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<IEnumerable<DisputeRequestDto>> HandleAsync(GetDisputeRequestByMandalamId query)
    {
        var mandalamId = new GenericId(query.MandalamId);
        var disputeRequests = await _dbContext.DisputeRequests
            .Include(x => x.ProposedArea).ThenInclude(x => x.State)
            .Include(x => x.ProposedMandalam).ThenInclude(x => x.District)
            .Include(x => x.ProposedPanchayat).ThenInclude(x => x.Mandalam)
            .AsNoTracking()
            .Where(x => x.ProposedMandalamId == mandalamId)
            .Select(x => x.AsDto())
            .ToListAsync();

        return disputeRequests;
    }
}