namespace Membership.Core.Exceptions.Users;

public class InvalidRoleException : CustomException
{
    public string Role { get; }
    public InvalidRoleException(string role) : base($"Invalid user role : {role}.")
    {
        Role = role;
    }
}