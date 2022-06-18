using Membership.Core.Exceptions;

namespace Membership.Application.Exceptions.Users;

public class NotAuthorizedRoleException : CustomException
{
    public string RoleName { get; }
    
    public NotAuthorizedRoleException(string roleName) : base($"Not authorized to create role {roleName}.")
    {
        RoleName = roleName;
    }
}