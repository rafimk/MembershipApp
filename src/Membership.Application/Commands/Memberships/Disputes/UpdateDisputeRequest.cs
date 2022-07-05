using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Memberships.Disputes;

public record UpdateDisputeRequest : ICommand
{
    public Guid Id { get; set; }
    public Guid ProposedAreaId { get; set; }
    public string Reason { get; set; }
}