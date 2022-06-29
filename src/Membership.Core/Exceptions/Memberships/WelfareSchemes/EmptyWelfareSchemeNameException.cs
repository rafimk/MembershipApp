
namespace Membership.Core.Exceptions.Memberships.RegisteredOrganizations;

public class EmptyWelfareSchemeNameException : CustomException
{
    public EmptyWelfareSchemeNameException() : base("Welfare scheme name name cannot be empty.")
    {
    }
}
