using Membership.Core.Exceptions;

namespace Membership.Application.Exceptions.Memberships;


public class DisputeReasonRequired : CustomException
{
    
    public DisputeReasonRequired() : base("Dispute Reason Required.")
    {
    }
}