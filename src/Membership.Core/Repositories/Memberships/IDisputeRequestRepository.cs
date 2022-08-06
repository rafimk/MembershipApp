using Membership.Core.Entities.Memberships.Disputes;

namespace Membership.Core.Repositories.Memberships;

public interface IDisputeRequestRepository
{
    Task<DisputeRequest> GetByIdAsync(Guid id);
    Task<DisputeRequest> GetByMemberIdAsync(Guid id);
    Task<IEnumerable<DisputeRequest>> GetAsync();
    Task AddAsync(DisputeRequest request);
    Task UpdateAsync(DisputeRequest request);
}