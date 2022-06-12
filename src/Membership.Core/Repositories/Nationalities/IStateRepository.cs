using Membership.Core.Entities.Nationalities;
using Membership.Core.ValueObjects;

namespace Membership.Core.Repositories.Nationalities;

public interface IStateRepository
{
    Task<State> GetByIdAsync(GenericId id);
    Task<IEnumerable<State>> GetAsync();
    Task AddAsync(State state);
    Task UpdateAsync(State state);
}
