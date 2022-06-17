using Membership.Application.Abstractions;
using Membership.Application.Queries.Users;
using Membership.Core.Policies.Users;
using Membership.Core.ValueObjects;

namespace Membership.Infrastructure.DAL.Handlers.Users;

internal sealed class GetApplicableUserRoleHandler : IQueryHandler<GetApplicableUserRole, IEnumerable<string>>
{
    private readonly IUserCreatePolicy _userCreatePolicy;
    
    public GetApplicableUserRoleHandler(IUserCreatePolicy userCreatePolicy)
        => _userCreatePolicy = userCreatePolicy;
    
    public async Task<IEnumerable<string>> HandleAsync(GetApplicableUserRole query)
    {
        var currentUserRole = UserRole.CentralCommitteeAdmin();
        if (!_userCreatePolicy.CanBeApplied(currentUserRole))
        {
            return null;
        }

        return _userCreatePolicy.PermittedUserRole(currentUserRole).Select(x => x.ToString());
    }
}