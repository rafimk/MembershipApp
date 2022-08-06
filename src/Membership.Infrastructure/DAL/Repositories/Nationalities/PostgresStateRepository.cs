using Membership.Core.Entities.Nationalities;
using Membership.Core.Repositories.Nationalities;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.DAL.Repositories.Nationalities;

internal sealed class PostgresStateRepository : IStateRepository
{
    private readonly DbSet<State> _states;

    public PostgresStateRepository(MembershipDbContext dbContext)
    {
        _states = dbContext.States;
    }

    public  Task<State> GetByIdAsync(Guid id)
        => _states.SingleOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<State>> GetAsync()
        => await _states.Where(x => !x.IsDeleted).ToListAsync();

    public async Task AddAsync(State states) 
        => await _states.AddAsync(states);

    public async Task UpdateAsync(State states)
    {
        _states.Update(states);
        await Task.CompletedTask;
    }
}