namespace Membership.Core.Exceptions.Nationalities.States;
public class EmptyStateNameException : CustomException
{
    public EmptyStateNameException() : base("State name cannot be empty.")
    {
    }
}
