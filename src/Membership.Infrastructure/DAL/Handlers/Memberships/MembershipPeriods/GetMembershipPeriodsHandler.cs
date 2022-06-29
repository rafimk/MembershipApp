using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;
using Membership.Application.Queries.Memberships.MembershipPeriods;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.MembershipPeriods;

internal sealed class GetMembershipPeriodsHandler : IQueryHandler<GetMembershipPeriods, IEnumerable<MembershipPeriodDto>>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetMembershipPeriodsHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<IEnumerable<MembershipPeriodDto>> HandleAsync(GetMembershipPeriods query)
        => await _dbContext.MembershipPeriods
            .OrderBy(x => x.Start);
            .Where(x => x.IsActive)
            .AsNoTracking()
            .Select(x => x.AsDto())
            .ToListAsync();
}