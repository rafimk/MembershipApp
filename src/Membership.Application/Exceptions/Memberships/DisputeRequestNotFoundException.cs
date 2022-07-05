using Membership.Core.Exceptions;

namespace Membership.Application.Exceptions.Memberships;

public class DisputeRequestNotFoundException : CustomException
{
    public Guid? Id { get; }
    
    public DisputeRequestNotFoundException(Guid? id) : base($"Dispute request Id {id} not found.")
    {
        Id = id;
    }
}