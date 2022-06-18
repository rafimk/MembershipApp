using Membership.Application.Abstractions;
using Membership.Application.Queries.Users;
using Membership.Core.Entities.Users;
using Membership.Core.Policies.Users;
using Membership.Core.Repositories.Users;
using Membership.Core.ValueObjects;
using Membership.Infrastructure.DAL.Exceptions;

namespace Membership.Infrastructure.DAL.Handlers.Users;

internal sealed class GetApplicableUserRoleHandler : IQueryHandler<GetApplicableUserRole, IEnumerable<string>>
{
    private readonly IEnumerable<IUserCreatePolicy> _policies;
    private readonly IUserRepository _userRepository;

    public GetApplicableUserRoleHandler(IEnumerable<IUserCreatePolicy> policies, IUserRepository userRepository)
    {
        _policies = policies;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<string>> HandleAsync(GetApplicableUserRole query)
    {
        var user = await _userRepository.GetByIdAsync(query.UserId);

        if (user is null)
        {
            return null;
        }
        
        var currentUserRole = user.Role;
        
        var policy = _policies.SingleOrDefault(x => x.CanBeApplied(currentUserRole));

        if (policy is null)
        {
            throw new NoUserCreatePolicyFoundException(currentUserRole.ToString());
        }
 
        var roles = policy.PermittedUserRole(currentUserRole).Select(x => x.ToString());
  
        return roles;
    }
}