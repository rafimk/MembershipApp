using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Memberships.Disputes;

public record CreateDisputeRequest() : ICommand
{
    public Guid? Id { get; set; }
    public Guid MemberId { get; set; }
    public Guid ProposedAreaId { get; set; }
    public Guid ProposedPanchayatId { get; set; }
    public string Reason { get; set; }
    public Guid? SubmittedBy { get; set; }
}