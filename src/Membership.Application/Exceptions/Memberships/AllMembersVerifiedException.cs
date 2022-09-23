using Membership.Core.Exceptions;

namespace Membership.Application.Exceptions.Memberships;

public class AllMembersVerifiedException : CustomException
{
    
    public AllMembersVerifiedException() : base("All members verified.")
    {
    }
}