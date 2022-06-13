namespace Membership.Application.Exceptions.Memberships;

public class ProfessionNotFoundException : MMSException
{
    public Guid Id { get; }
    
    public ProfessionNotFoundException(Guid id) : base($"Profession Id {id} not found.")
    {
        Id = id;
    }
}