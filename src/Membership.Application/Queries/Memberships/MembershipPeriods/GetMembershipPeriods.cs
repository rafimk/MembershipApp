using Membership.Application.Abstractions;
using Membership.Application.DTO.Memberships;

namespace Membership.Application.Queries.Memberships.MembershipPeriods;

public class GetMembershipPeriods : IQuery<IEnumerable<MembershipPeriodDto>>
{
}