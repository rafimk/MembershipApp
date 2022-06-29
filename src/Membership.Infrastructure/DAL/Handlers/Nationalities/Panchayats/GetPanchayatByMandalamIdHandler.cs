using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;
using Membership.Application.Queries.Nationalities.Panchayats;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Nationalities.Panchayats;

internal sealed class GetPanchayatByMandalamIdHandler : IQueryHandler<GetPanchayatByMandalamId, IEnumerable<PanchayatDto>>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetPanchayatByMandalamIdHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<IEnumerable<PanchayatDto>> HandleAsync(GetPanchayatByMandalamId query)
    {
        var mandalamId = new GenericId(query.MandalamId);
        return await _dbContext.Panchayats
            .OrderBy(x => x.Name)
            .Include(x => x.Mandalam)
            .OrderBy(x => x.Name)
            .AsNoTracking()
            .Where(x => x.MandalamId == mandalamId)
            .Select(x => x.AsDto())
            .ToListAsync();;
    }
}