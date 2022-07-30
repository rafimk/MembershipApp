using Membership.Core.Exceptions;

namespace Membership.Application.Exceptions.Users;

public class PasswordAndConfirmPasswordMissmatchException : CustomException
{
    public PasswordAndConfirmPasswordMissmatchException() : base("Password and confirmed password mismatch.")
    {
    }
}
