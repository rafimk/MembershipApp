namespace Membership.Core.Exceptions.Memberships;

public class InvalidEmiratesIdExpiryDate : CustomException
{
    public InvalidEmiratesIdExpiryDate() : base("Invalid emirates id  expiry date/expired.")
    {
    }
}