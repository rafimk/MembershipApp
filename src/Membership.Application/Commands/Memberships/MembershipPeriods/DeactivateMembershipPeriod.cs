using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Memberships.MembershipPeriods;

public record DeactivateMembershipPeriod() : ICommand
{
    public Guid? MembershipPeriodId { get; set; }
}