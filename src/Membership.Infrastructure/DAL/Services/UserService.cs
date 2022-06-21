using Membership.Core.DomainServices.Users;
using Membership.Core.Policies.Users;
using Membership.Core.Repositories.Users;
using Membership.Core.ValueObjects;
using Membership.Infrastructure.DAL.Exceptions;

namespace Membership.Infrastructure.DAL.Services;

internal sealed class UserService : IUserService
{
    private readonly IEnumerable<IUserCreatePolicy> _policies;
    private readonly IUserRepository _userRepository;
   
    public UserService(IEnumerable<IUserCreatePolicy> policies, IUserRepository userRepository)
    {
        _policies = policies;
        _userRepository = userRepository;
    }
    
    public async Task<IEnumerable<string>> GetApplicableUserRolesAsync(UserRole role, Guid? userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);

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

    public async Task<Guid?> GetStateId(Guid? cascadeId, Guid? parentUserId)
    {
        var user = await _userRepository.GetByIdAsync(parentUserId);

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
 
        return policy.GetStateId(cascadeId, user.StateId);
    }
}