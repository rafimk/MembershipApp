using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;
using Membership.Application.Queries.Nationalities;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Nationalities.Areas;

internal sealed class GetAreasHandler : IQueryHandler<GetAreas, IEnumerable<AreatDto>>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetAreasHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<IEnumerable<AreatDto>> HandleAsync(GetAreas query)
        => await _dbContext.Areas
            .Include(x => x.State)
            .AsNoTracking()
            .Select(x => x.AsDto())
            .ToListAsync();
}