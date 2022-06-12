namespace Membership.Core.Exceptions.Nationalities.Areas;

public class EmptyAreaNameException : CustomException
{
    public EmptyAreaNameException() : base("Area name cannot be empty.")
    {
    }
}
