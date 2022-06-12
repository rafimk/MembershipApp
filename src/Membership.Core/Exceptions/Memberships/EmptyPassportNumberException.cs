namespace Membership.Core.Exceptions.Memberships;

public class EmptyPassportNumberException : CustomException
{
    public EmptyPassportNumberException() : base("Passport number cannot be empty.")
    {
    }
}
