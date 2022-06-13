using Membership.Core.Exceptions;

namespace Membership.Application.Exceptions.Nationalities;

public class StateNotFoundException : CustomException
{
    public Guid? Id { get; }
    
    public StateNotFoundException(Guid? id) : base($"State Id {id} not found.")
    {
        Id = id;
    }
}