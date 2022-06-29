using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Memberships.MembershipPeriods;

public record EnrollActivateMembershipPeriod() : ICommand
{
    public Guid? MembershipPeriodId { get; set; }
}