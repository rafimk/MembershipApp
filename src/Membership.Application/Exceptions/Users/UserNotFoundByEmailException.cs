using Membership.Core.Exceptions;

namespace Membership.Application.Exceptions.Users;

public class UserNotFoundByEmailException : CustomException
{
    public string Email { get; }
    
    public UserNotFoundByEmailException(string email) : base($"User email {email} not found.")
    {
        Email = email;
    }
}