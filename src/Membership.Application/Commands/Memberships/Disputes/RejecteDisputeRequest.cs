using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Memberships.Disputes;

public record RejecteDisputeRequest() : ICommand
{
    public Guid RequestId { get; set;}
    public string JustificationComment { get; set; }
}