using Membership.Core.Exceptions;

namespace Membership.Infrastructure.DAL.Exceptions;

public class NoWidgetPolicyFoundException : CustomException
{
    public string UserRole { get; }
    
    public NoWidgetPolicyFoundException(string userRole) : base($"No widget policy policy found for : {userRole}.")
    {
        UserRole = userRole;
    }
}