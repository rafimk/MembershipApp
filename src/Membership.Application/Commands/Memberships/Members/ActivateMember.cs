using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Memberships.Members;

public record ActivateMember() : ICommand
{
    public Guid MemberId { get; set;}
}