using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;
using Membership.Application.Queries.Nationalities;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Nationalities.Districts;

internal sealed class GetDistrictsHandler : IQueryHandler<GetDistricts, IEnumerable<DistrictDto>>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetDistrictsHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<IEnumerable<DistrictDto>> HandleAsync(GetDistricts query)
        => await _dbContext.Districts
            .AsNoTracking()
            .Select(x => x.AsDto())
            .ToListAsync();
}