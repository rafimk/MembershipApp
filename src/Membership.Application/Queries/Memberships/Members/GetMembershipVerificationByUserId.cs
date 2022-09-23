using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;

namespace Membership.Application.Queries.Memberships.Members;

public record GetMembershipVerificationByUserId : IQuery<MembershipVerificationDto>
{
    public Guid Id { get; set; }
    public Guid? UserId { get; set; }
}