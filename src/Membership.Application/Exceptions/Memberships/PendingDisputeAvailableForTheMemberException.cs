using Membership.Core.Exceptions;

namespace Membership.Application.Exceptions.Memberships;

public class PendingDisputeAlreadyAvailableException : CustomException
{
    
    public PendingDisputeAlreadyAvailableException() : base($"Pending dispute already available for the member")
    {
    }
}