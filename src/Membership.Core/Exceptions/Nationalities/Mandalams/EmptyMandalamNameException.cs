namespace Membership.Core.Exceptions.Nationalities.Mandalams;

public class EmptyMandalamNameException : CustomException
{
    public EmptyMandalamNameException() : base("Mandalam name cannot be empty.")
    {
    }
}
