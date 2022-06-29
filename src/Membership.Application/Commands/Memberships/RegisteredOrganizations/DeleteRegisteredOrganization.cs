using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Memberships.RegisteredOrganizations;

public record DeleteRegisteredOrganization() : ICommand
{
    public Guid? RegisteredOrganizationId { get; set;}
}