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
            .Include(x => x.Member).ThenInclude(x => x.Area)
            .Include(x => x.Member).ThenInclude(x => x.Mandalam).ThenInclude(x => x.District)
            .Include(x => x.Member).ThenInclude(x => x.Panchayat)
            .Include(x => x.Member).ThenInclude(x => x.Qualification)
            .Include(x => x.Member).ThenInclude(x => x.Profession)
            .Include(x => x.Member).ThenInclude(x => x.MembershipPeriod)
            .Include(x => x.ProposedArea).ThenInclude(x => x.State)
            .Include(x => x.ProposedMandalam).ThenInclude(x => x.District)
            .Include(x => x.ProposedPanchayat).ThenInclude(x => x.Mandalam)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == requestId);

        return disputeRequest?.AsDtoWithMember();
    }
}