using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.MembershipPeriods;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Membership.MembershipPeriods;

internal sealed class GetMembershipPeriodsHandler : IQueryHandler<GetMembershipPeriods, IEnumerable<MembershipPeriodDto>>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetMembershipPeriodsHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<IEnumerable<MembershipPeriodDto>> HandleAsync(GetMembershipPeriods query)
        => await _dbContext.MembershipPeriods
            .Where(x => !x.IsDelete)
            .AsNoTracking()
            .Select(x => x.AsDto())
            .ToListAsync();
}