using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.MembershipPeriods;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.MembershipPeriods;

internal sealed class GetMembershipPeriodActiveHandler : IQueryHandler<GetMembershipPeriodActive, MembershipPeriodDto>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetMembershipPeriodActiveHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<MembershipPeriodDto> HandleAsync(GetMembershipPeriodActive query)
    {
        var membershipPeriod = await _dbContext.MembershipPeriods
            .AsNoTracking()
            .FirstAsync(x => x.IsActive);

        return membershipPeriod?.AsDto();
    }
}