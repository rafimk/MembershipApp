using Membership.Core.Exceptions;

namespace Membership.Application.Exceptions.Memberships;

public class ProfessionNotFoundException : CustomException
{
    public Guid? Id { get; }
    
    public ProfessionNotFoundException(Guid? id) : base($"Profession Id {id} not found.")
    {
        Id = id;
    }
}