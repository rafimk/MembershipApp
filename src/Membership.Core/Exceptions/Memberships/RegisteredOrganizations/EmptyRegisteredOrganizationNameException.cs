
namespace Membership.Core.Exceptions.Memberships.RegisteredOrganizations;

public class EmptyRegisteredOrganizationNameException : CustomException
{
    public EmptyRegisteredOrganizationNameException() : base("Registered organization name cannot be empty.")
    {
    }
}
