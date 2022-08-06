using Membership.Core.Entities.Memberships.WelfareSchemes;

namespace Membership.Core.Repositories.Memberships;

public interface IWelfareSchemeRepository
{
    Task<WelfareScheme> GetByIdAsync(Guid id);
    Task<IEnumerable<WelfareScheme>> GetAsync();
    Task AddAsync(WelfareScheme welfareScheme);
    Task UpdateAsync(WelfareScheme welfareScheme);
    Task DeleteAsync(Guid id);
}