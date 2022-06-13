using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;
using Membership.Application.Queries.Nationalities;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Nationalities.Areas;

internal sealed class GetMandalamByDistrictIdHandler : IQueryHandler<GetMandalamByDistrictId, MandalamDto>
{
    private readonly GetMandalamByDistrictIdHandler _dbContext;
    
    public GetAreaByStateIdHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<MandalamDto> HandleAsync(GetMandalamByDistrictId query)
    {
        var districtId = new GenericId(query.DistrictId);
        var mandalam = await _dbContext.Mandalams
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.DistrictId == districtId);

        return mandalam?.AsDto();
    }
}