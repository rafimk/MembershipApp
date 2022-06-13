namespace Membership.Application.Exceptions.Memberships;

public class QualificationNotFoundException : MMSException
{
    public Guid Id { get; }
    
    public QualificationNotFoundException(Guid id) : base($"Qualification Id {id} not found.")
    {
        Id = id;
    }
}