using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;
using Membership.Application.Queries.Nationalities;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Nationalities.Areas;

internal sealed class GetAreaByStateIdHandler : IQueryHandler<GetAreaByStateId, AreaDto>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetAreaByStateIdHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<AreaDto> HandleAsync(GetAreaByStateId query)
    {
        var stateId = new GenericId(query.StateId);
        var area = await _dbContext.Areas
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.StateId == stateId);

        return area?.AsDto();
    }
}