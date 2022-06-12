namespace Membership.Core.Exceptions.Memberships;

public class InvalidDateOfBirthException : CustomException
{
    public InvalidDateOfBirthException() : base("The date of birth can not be in the future.")
    {
    }
}