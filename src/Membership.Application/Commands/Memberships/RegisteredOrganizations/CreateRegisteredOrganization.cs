using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Memberships.RegisteredOrganizations;

public record CreateRegisteredOrganization() : ICommand
{
    public Guid RegisteredOrganizationId { get; set; }
    public string Name{ get; set; }
}