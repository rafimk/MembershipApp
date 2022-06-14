using Membership.Application.Abstractions;
using Membership.Application.DTO.Nationalities;

namespace Membership.Application.Queries.Memberships.MembershipPeriods;

public class GetMembershipPeriodById : IQuery<MembershipPeriodDto>
{
    public Guid MembershipPeriodId { get; set; }
}