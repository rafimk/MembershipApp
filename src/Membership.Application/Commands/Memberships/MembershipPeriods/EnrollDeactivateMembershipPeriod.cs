using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Memberships.MembershipPeriods;

public record EnrollDeactivateMembershipPeriod() : ICommand
{
    public Guid? MembershipPeriodId { get; set; }
}