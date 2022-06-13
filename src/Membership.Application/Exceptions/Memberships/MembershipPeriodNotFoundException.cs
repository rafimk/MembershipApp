using Membership.Core.Exceptions;

namespace Membership.Application.Exceptions.Memberships;

public class MembershipPeriodNotFoundException : CustomException
{
    public Guid? Id { get; }
    
    public MembershipPeriodNotFoundException(Guid? id) : base($"Membership period Id {id} not found.")
    {
        Id = id;
    }
}