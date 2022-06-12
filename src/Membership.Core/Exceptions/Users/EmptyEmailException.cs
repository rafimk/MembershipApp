namespace Membership.Core.Exceptions.Users;

public class EmptyEmailException : CustomException
{
    public EmptyEmailException() : base("Email cannot be empty.")
    {
    }
}