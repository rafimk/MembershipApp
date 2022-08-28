namespace Membership.Core.Exceptions.Users;

public class TooManyFailedLoginAttempts : CustomException
{
    public TooManyFailedLoginAttempts() : base("Too many failed login attempts. Please try again in 60 minutes.")
    {
    }
}