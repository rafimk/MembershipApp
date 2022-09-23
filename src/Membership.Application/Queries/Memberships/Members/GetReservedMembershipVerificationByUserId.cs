using Membership.Application.Abstractions;

namespace Membership.Application.Queries.Memberships.Members;

public record GetReservedMembershipVerificationByUserId : IQuery<Guid?>
{
    public Guid? UserId { get; set; }
}