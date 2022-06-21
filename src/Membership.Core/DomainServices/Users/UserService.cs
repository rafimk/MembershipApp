using Membership.Core.Entities.Users;
using Membership.Core.ValueObjects;

namespace Membership.Core.DomainServices.Users;

internal sealed class UserService : IUserService
{
    private readonly IEnumerable<IUserCreatePolicy> _policies;
    private readonly IUserRepository _userRepository;
   
    public UserService(Parameters)
    {
        
    }
    
    public Task<IEnumerable<string>> GetApplicableUserRolesAsync(UserRole role, string userId)
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

    public Guid? GetStateId(Guid? cascadeId, Guid? parentUserId)
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