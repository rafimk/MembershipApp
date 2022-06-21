using Membership.Core.Exceptions;

namespace Membership.Application.Exceptions.Memberships;

public class NotAuthorizedToCreateMemberForThisAreaException : CustomException
{
    
    public NotAuthorizedToCreateMemberForThisAreaException() : base($"NotAuthorized to create member for selected area.")
    {
    }
}