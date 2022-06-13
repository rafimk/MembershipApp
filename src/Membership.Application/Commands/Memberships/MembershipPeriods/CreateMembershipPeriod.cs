using Membership.Application.Abstractions;

namespace Membership.Application.Commands.Memberships.MembershipPeriods;

public record CreateMembershipPeriod() : ICommand
{
    public DateTimeOffset Start { get; set; }
    public DateTimeOffset End {get; set;}
    public DateTimeOffset RegistrationUntil { get; set; }
}