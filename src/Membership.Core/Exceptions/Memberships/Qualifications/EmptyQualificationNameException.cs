namespace Membership.Core.Exceptions.Memberships.Qualifications;

public class EmptyQualificationNameException : CustomException
{
    public EmptyQualificationNameException() : base("Qualification name cannot be empty.")
    {
    }
}
