using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;
using Membership.Application.Queries.Nationalities;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Nationalities.Districts;

internal sealed class GetDistrictByIdHandler : IQueryHandler<GetDistrictById, DistrictDto>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetDistrictByIdHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<DistrictDto> HandleAsync(GetDistrictById query)
    {
        var stateId = new GenericId(query.StateId);
        var state = await _dbContext.Districts
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == stateId);

        return state?.AsDto();
    }
}