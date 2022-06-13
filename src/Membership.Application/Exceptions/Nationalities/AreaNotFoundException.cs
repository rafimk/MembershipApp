using Membership.Core.Exceptions;

namespace Membership.Application.Exceptions.Nationalities;

public class AreaNotFoundException : CustomException
{
    public Guid? Id { get; }
    
    public AreaNotFoundException(Guid? id) : base($"Area Id {id} not found.")
    {
        Id = id;
    }
}