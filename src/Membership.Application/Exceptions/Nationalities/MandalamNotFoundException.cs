namespace Membership.Application.Exceptions.Nationalities;

public class MandalamNotFoundException : MMSException
{
    public Guid Id { get; }
    
    public MandalamNotFoundException(Guid id) : base($"Mandalam Id {id} not found.")
    {
        Id = id;
    }
}