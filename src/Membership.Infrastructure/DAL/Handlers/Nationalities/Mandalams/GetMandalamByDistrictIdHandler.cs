using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;
using Membership.Application.Queries.Nationalities.Mandalams;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Nationalities.Mandalams;

internal sealed class GetMandalamByDistrictIdHandler : IQueryHandler<GetMandalamByDistrictId, IEnumerable<MandalamDto>>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetMandalamByDistrictIdHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<IEnumerable<MandalamDto>> HandleAsync(GetMandalamByDistrictId query)
    {
        var districtId = new GenericId(query.DistrictId);
        return await _dbContext.Mandalams
            .Include(x => x.District)
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Where(x => x.DistrictId == districtId).Select(x => x.AsDto())
            .ToListAsync();;
    }
}