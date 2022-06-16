using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;
using Membership.Application.Queries.Memberships;
using Membership.Application.Queries.Memberships.MembershipPeriods;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Memberships.MembershipPeriod;

internal sealed class GetMembershipPeriodByIdHandler : IQueryHandler<GetMembershipPeriodById, MembershipPeriodDto>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetMembershipPeriodByIdHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<MembershipPeriodDto> HandleAsync(GetMembershipPeriodById query)
    {
        var membershipPeriodId = new GenericId(query.MembershipPeriodId);
        var membershipPeriod = await _dbContext.MembershipPeriodd
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == membershipPeriodId);

        return membershipPeriod?.AsDto();
    }
}