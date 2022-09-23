using Membership.Core.Exceptions;

namespace Membership.Application.Exceptions.Memberships;

public class VerificationReservationNotFoundException : CustomException
{
    public Guid? Id { get; }
    
    public VerificationReservationNotFoundException(Guid? id) : base($"Verification reservation {id} not found.")
    {
        Id = id;
    }
}