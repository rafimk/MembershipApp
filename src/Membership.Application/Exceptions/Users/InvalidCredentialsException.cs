using Membership.Core.Exceptions;

namespace Membership.Application.Exceptions.Users;

public class InvalidCredentialsException : CustomException
{
    public InvalidCredentialsException() : base("Invalid credentials.")
    {
    }
}