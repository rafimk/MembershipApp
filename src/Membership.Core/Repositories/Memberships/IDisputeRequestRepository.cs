using Membership.Core.Entities.Memberships.Disputes;
using Membership.Core.ValueObjects;

namespace Membership.Core.Repositories.Memberships;

public interface IDisputeRequestRepository
{
    Task<DisputeRequest> GetByIdAsync(GenericId id);
    Task<DisputeRequest> GetByMemberIdAsync(GenericId id);
    Task<IEnumerable<DisputeRequest>> GetAsync();
    Task AddAsync(DisputeRequest request);
    Task UpdateAsync(DisputeRequest request);
}