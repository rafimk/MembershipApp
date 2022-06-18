using Membership.Core.Exceptions;

namespace Membership.Infrastructure.DAL.Exceptions;

public class NoUserCreatePolicyFoundException : CustomException
{
    public string RoleName { get; }
    
    public NoUserCreatePolicyFoundException(string roleName) : base($"No user create policy found for the role : {roleName}.")
    {
        RoleName = roleName;
    }
}