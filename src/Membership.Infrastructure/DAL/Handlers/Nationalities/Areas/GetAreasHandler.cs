using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;
using Membership.Application.Queries.Nationalities;
using Membership.Application.Queries.Nationalities.Areas;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Nationalities.Areas;

internal sealed class GetAreasHandler : IQueryHandler<GetAreas, IEnumerable<AreaDto>>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetAreasHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<IEnumerable<AreaDto>> HandleAsync(GetAreas query)
        => await _dbContext.Areas
            .Include(x => x.State)
            .Where(x => !x.IsDelete)
            .AsNoTracking()
            .Select(x => x.AsDto())
            .ToListAsync();
}