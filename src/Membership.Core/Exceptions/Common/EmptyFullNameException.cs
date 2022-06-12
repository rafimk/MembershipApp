namespace Membership.Core.Exceptions.Common;

public class EmptyFullNameException : CustomException
{
    public EmptyFullNameException() : base("Full name cannot be empty.")
    {
    }
}