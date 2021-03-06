using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;
using Membership.Application.Queries.Nationalities.Panchayats;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Nationalities.Panchayats;

internal sealed class GetPanchayatsHandler : IQueryHandler<GetPanchayats, IEnumerable<PanchayatDto>>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetPanchayatsHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<IEnumerable<PanchayatDto>> HandleAsync(GetPanchayats query)
        => await _dbContext.Panchayats
            .OrderBy(x => x.Name)
            .Include(x => x.Mandalam)
            .AsNoTracking()
            .Select(x => x.AsDto())
            .ToListAsync();
}