using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;
using Membership.Application.Queries.Nationalities;
using Membership.Application.Queries.Nationalities.Areas;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Nationalities.Areas;

internal sealed class GetAreaByStateIdHandler : IQueryHandler<GetAreaByStateId, IEnumerable<AreaDto>>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetAreaByStateIdHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<IEnumerable<AreaDto>> HandleAsync(GetAreaByStateId query)
    {
        var stateId = new GenericId(query.StateId);
        return await _dbContext.Areas
            .AsNoTracking()
            .Where(x => x.StateId == stateId).Select(x => x.AsDto())
            .ToListAsync();
    }
}