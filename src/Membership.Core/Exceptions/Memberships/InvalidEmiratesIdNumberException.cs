namespace Membership.Core.Exceptions.Memberships;

public class InvalidEmiratesIdNumberException : CustomException
{
    public InvalidEmiratesIdNumberException() : base("Invalid emirates id number.")
    {
    }
}