
namespace Membership.Core.Exceptions.Memberships.Professions;

public class EmptyProfessionNameException : CustomException
{
    public EmptyProfessionNameException() : base("Profession name cannot be empty.")
    {
    }
}
