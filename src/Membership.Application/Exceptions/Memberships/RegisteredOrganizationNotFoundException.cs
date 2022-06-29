using Membership.Core.Exceptions;

namespace Membership.Application.Exceptions.Memberships;

public class RegisteredOrganizationNotFoundException : CustomException
{
    public Guid? Id { get; }
    
    public RegisteredOrganizationNotFoundException(Guid? id) : base($"Registered organization Id {id} not found.")
    {
        Id = id;
    }
}