using Membership.Core.Exceptions;

namespace Membership.Application.Exceptions.Memberships;

public class MemberNotFoundException : CustomException
{
    public Guid? Id { get; }
    
    public MemberNotFoundException(Guid? id) : base($"Member Id {id} not found.")
    {
        Id = id;
    }
}