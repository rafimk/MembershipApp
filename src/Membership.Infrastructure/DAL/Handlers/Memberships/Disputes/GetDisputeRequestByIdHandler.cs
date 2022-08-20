using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.Disputes;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.Disputes;

internal sealed class GetDisputeRequestByIdHandler : IQueryHandler<GetDisputeRequestById, DisputeRequestDto>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetDisputeRequestByIdHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<DisputeRequestDto> HandleAsync(GetDisputeRequestById query)
    {
        var requestId = query.RequestId;
        var disputeRequest = await _dbContext.DisputeRequests
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
            .SingleOrDefaultAsync(x => x.Id == requestId);

        return disputeRequest?.AsDtoWithMember();
    }
}