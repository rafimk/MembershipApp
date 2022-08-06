using Membership.Core.Entities.Nationalities;

namespace Membership.Core.Repositories.Nationalities;

public interface IStateRepository
{
    Task<State> GetByIdAsync(Guid id);
    Task<IEnumerable<State>> GetAsync();
    Task AddAsync(State state);
    Task UpdateAsync(State state);
}
