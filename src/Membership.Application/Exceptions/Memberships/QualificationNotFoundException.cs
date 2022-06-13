using Membership.Core.Exceptions;

namespace Membership.Application.Exceptions.Memberships;

public class QualificationNotFoundException : CustomException
{
    public Guid? Id { get; }
    
    public QualificationNotFoundException(Guid? id) : base($"Qualification Id {id} not found.")
    {
        Id = id;
    }
}