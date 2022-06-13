using Membership.Core.Exceptions;

namespace Membership.Application.Exceptions.Nationalities;

public class DistrictNotFoundException : CustomException
{
    public Guid? Id { get; }
    
    public DistrictNotFoundException(Guid? id) : base($"District Id {id} not found.")
    {
        Id = id;
    }
}