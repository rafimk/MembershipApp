using Membership.Core.Exceptions;

namespace Membership.Infrastructure.Exceptions;

public class NoUserDataReteivePolicyFoundException : CustomException
{
    public string UserRole { get; }
    
    public NoUserDataReteivePolicyFoundException(string userRole) : base($"No user data retrieve policy found for : {userRole}.")
    {
        UserRole = userRole;
    }
}