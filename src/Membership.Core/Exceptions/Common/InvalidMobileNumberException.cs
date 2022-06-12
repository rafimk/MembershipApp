namespace Membership.Core.Exceptions.Common;

public class InvalidMobileNumberException : CustomException
{
    public InvalidMobileNumberException() : base("Invalid mobile number.")
    {
    }
}