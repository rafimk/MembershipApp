namespace Membership.Core.Exceptions.Memberships;

public class EmptyEmiratesIdNumberException : CustomException
{
    public EmptyEmiratesIdNumberException() : base("Emirates Id Number cannot be empty.")
    {
    }
}