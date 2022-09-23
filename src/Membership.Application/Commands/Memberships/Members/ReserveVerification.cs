using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Memberships.Members;

public record ReserveVerification() : ICommand
{
    public Guid? Id { get; set; }
    public Guid? VerifiedUserId { get; set; }
}