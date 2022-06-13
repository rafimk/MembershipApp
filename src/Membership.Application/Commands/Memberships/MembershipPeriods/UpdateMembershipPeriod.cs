using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Memberships.MembershipPeriods;

public record UpdateMembershipPeriod() : ICommand
{
    public Guid? MembershipPeriodId { get; set; }
    public DateTimeOffset Start { get; set; }
    public DateTimeOffset End {get; set;}
    public DateTimeOffset RegistrationUntil { get; set; }
}