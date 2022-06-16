using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;
using Membership.Application.Queries.Nationalities;
using Membership.Application.Queries.Nationalities.States;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Handlers.Nationalities.States;

internal sealed class GetStatesHandler : IQueryHandler<GetStates, IEnumerable<StateDto>>
{
    private readonly MembershipDbContext _dbContext;
    
    public GetStatesHandler(MembershipDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<IEnumerable<StateDto>> HandleAsync(GetStates query)
        => await _dbContext.States
            .Where(x => !x.IsDelete)
            .AsNoTracking()
            .Select(x => x.AsDto())
            .ToListAsync();
}