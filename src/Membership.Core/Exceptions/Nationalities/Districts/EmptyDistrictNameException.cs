namespace Membership.Core.Exceptions.Nationalities.Districts;

public class EmptyDistrictNameException : CustomException
{
    public EmptyDistrictNameException() : base("District name cannot be empty.")
    {
    }
}
