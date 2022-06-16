using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;
using Membership.Application.Queries.Nationalities.Mandalams;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Nationalities.Mandalams;

internal sealed class GetMandalamsHandler : IQueryHandler<GetMandalams, IEnumerable<MandalamDto>>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetMandalamsHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<IEnumerable<MandalamDto>> HandleAsync(GetMandalams query)
        => await _dbContext.Mandalams
            .Include(x => x.District)
            .Where(x => !x.IsDelete)
            .AsNoTracking()
            .Select(x => x.AsDto())
            .ToListAsync();
}