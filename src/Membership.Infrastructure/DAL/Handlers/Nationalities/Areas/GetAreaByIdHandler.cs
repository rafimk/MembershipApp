using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;
using Membership.Application.Queries.Nationalities;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Nationalities.Areas;

internal sealed class GetAreaByIdHandler : IQueryHandler<GetAreaById, AreaDto>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetAreaByIdHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<AreaDto> HandleAsync(GetAreaById query)
    {
        var areaId = new GenericId(query.AreaId);
        var area = await _dbContext.Areas
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == stateId);

        return area?.AsDto();
    }
}