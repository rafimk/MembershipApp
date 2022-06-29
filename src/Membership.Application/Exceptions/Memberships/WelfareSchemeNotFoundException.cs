using Membership.Core.Exceptions;

namespace Membership.Application.Exceptions.Memberships;

public class WelfareSchemeNotFoundException : CustomException
{
    public Guid? Id { get; }
    
    public WelfareSchemeNotFoundException(Guid? id) : base($"Welfare scheme Id {id} not found.")
    {
        Id = id;
    }
}