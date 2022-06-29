using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Memberships.MembershipPeriods;

public record CreateMembershipPeriod() : ICommand
{
    public Guid MembershipPeriodId { get; set; }
    public DateTimeOffset Start { get; set; }
    public DateTimeOffset End {get; set;}
}