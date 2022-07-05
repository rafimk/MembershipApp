using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.Disputes;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.Disputes;

internal sealed class GetDisputeRequestByIdHandler : IQueryHandler<GetDisputeRequestById, DisputeRequestDto>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetDisputeRequestByIdHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<DisputeRequestDto> HandleAsync(GetDisputeRequestById query)
    {
        var requestId = new GenericId(query.RequestId);
        var disputeRequest = await _dbContext.DisputeRequests
            .Include(x => x.ProposedArea).ThenInclude(x => x.State)
            .Include(x => x.ProposedMandalam)
            .Include(x => x.ProposedPanchayat)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == requestId);

        return disputeRequest?.AsDto();
    }
}