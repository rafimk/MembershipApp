namespace Membership.Core.Exceptions.Memberships;

public class EmptyMembershipIdException : CustomException
{
    public EmptyMembershipIdException() : base("Membership Id cannot be empty.")
    {
    }
}