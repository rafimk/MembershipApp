namespace Membership.Core.Exceptions.Nationalities.Panchayats;

public class EmptyPanchayatNameException : CustomException
{
    public EmptyPanchayatNameException() : base("Panchayat name cannot be empty.")
    {
    }
}
