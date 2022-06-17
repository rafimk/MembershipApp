using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Memberships.Members;

public record DisputedMember() : ICommand
{
    public Guid MemberId { get; set; } 
    public Guid StateId { get; set; }
}