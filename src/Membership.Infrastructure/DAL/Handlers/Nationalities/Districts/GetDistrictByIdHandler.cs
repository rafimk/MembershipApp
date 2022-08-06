using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;
using Membership.Application.Queries.Nationalities.Districts;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Nationalities.Districts;

internal sealed class GetDistrictByIdHandler : IQueryHandler<GetDistrictById, DistrictDto>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetDistrictByIdHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<DistrictDto> HandleAsync(GetDistrictById query)
    {
        var districtId = query.DistrictId;
        var district = await _dbContext.Districts
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == districtId);

        return district?.AsDto();
    }
}