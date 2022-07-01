using Membership.Core.Exceptions;

namespace Membership.Application.Exceptions.Memberships;

public class ThereIsNoActiveMembershipPeriodAvailable : CustomException
{
    public ThereIsNoActiveMembershipPeriodAvailable() : base($"There is no active membership period available, please contact admin.")
    {
    }
}