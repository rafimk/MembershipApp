using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;
using Membership.Application.Queries.Nationalities;
using Membership.Application.Queries.Nationalities.States;
using Membership.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Nationalities.States;

internal sealed class GetStateByIdHandler : IQueryHandler<GetStateById, StateDto>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetStateByIdHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<StateDto> HandleAsync(GetStateById query)
    {
        var stateId = new GenericId(query.StateId);
        var state = await _dbContext.States
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == stateId);

        return state?.AsDto();
    }
}