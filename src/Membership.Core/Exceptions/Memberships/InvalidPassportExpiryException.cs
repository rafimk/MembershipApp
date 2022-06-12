namespace Membership.Core.Exceptions.Memberships;

public class InvalidPassportExpiryException : CustomException
{
    public InvalidPassportExpiryException() : base("Invalid passport expiry/expired")
    {
    }
}