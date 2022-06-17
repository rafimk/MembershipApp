using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Memberships.Members;

public record DeactivateMember(): ICommand
{
    public Guid MemberId { get; set;}
}