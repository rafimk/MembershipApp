using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.Members;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.Members;

internal sealed class GetDisputeRequestByRoleHandler : IQueryHandler<GetDisputeRequestByRole, DisputeRequestDto>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetDisputeRequestByRoleHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<DisputeRequestDto> HandleAsync(GetDisputeRequestByRole query)
    {
        var memberId = new GenericId(query.MemberId);
        var member = await _dbContext.Members
            .Include(x => x.Profession)
            .Include(x => x.Qualification)
            .Include(x => x.Mandalam)
            .Include(x => x.Panchayat)
            .Include(x => x.Area).ThenInclude(x => x.State)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == memberId);

        return member?.AsDto();
    }
}