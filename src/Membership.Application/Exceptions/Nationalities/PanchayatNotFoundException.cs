namespace Membership.Application.Exceptions.Nationalities;

public class PanchayatNotFoundException : MMSException
{
    public Guid Id { get; }
    
    public PanchayatNotFoundException(Guid id) : base($"Panchayat Id {id} not found.")
    {
        Id = id;
    }
}