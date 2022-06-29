using Membership.Core.Exceptions;

namespace Membership.Application.Exceptions.Memberships;

public class ActiveMembershipPeriodAlreadyExistException : CustomException
{
    
    public ActiveMembershipPeriodAlreadyExistException() : base($"Active membership period already exist.")
    {
    }
}