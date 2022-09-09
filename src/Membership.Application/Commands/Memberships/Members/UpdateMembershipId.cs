using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Memberships.Members;

public record UpdateMembershipId : ICommand
{
    public Guid Id { get; set; }
}