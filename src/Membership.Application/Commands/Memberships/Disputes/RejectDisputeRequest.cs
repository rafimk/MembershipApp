using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Memberships.Disputes;

public record RejectDisputeRequest() : ICommand
{
    public Guid RequestId { get; set;}
    public string JustificationComment { get; set; }
    public Guid? ActionBy { get; set; }
}