namespace Membership.Application.Exceptions.Memberships;

public class MembershipPeriodNotFoundException : MMSException
{
    public Guid Id { get; }
    
    public MembershipPeriodNotFoundException(Guid id) : base($"Membership period Id {id} not found.")
    {
        Id = id;
    }
}