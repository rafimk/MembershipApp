using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Memberships.Disputes;

public record UpdateDisputeRequest : ICommand
{
    public Guid Id { get; set; }
    public Guid ToAreaId { get; set; }
    public Guid ToPanchayatId { get; set; }
    public string Reason { get; set; }
    public Guid? SubmittedBy { get; set; }
}