namespace Membership.Application.Exceptions.Memberships;

public class MembershipNotFoundException : MMSException
{
    public Guid Id { get; }
    
    public MembershipNotFoundException(Guid id) : base($"Membership Id {id} not found.")
    {
        Id = id;
    }
}