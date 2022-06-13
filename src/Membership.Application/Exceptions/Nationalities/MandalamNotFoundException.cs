using Membership.Core.Exceptions;

namespace Membership.Application.Exceptions.Nationalities;

public class MandalamNotFoundException : CustomException
{
    public Guid? Id { get; }
    
    public MandalamNotFoundException(Guid? id) : base($"Mandalam Id {id} not found.")
    {
        Id = id;
    }
}