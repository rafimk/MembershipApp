using Membership.Core.Exceptions;

namespace Membership.Application.Exceptions.Memberships;

public class AgentNotFoundException : CustomException
{
    public Guid Id { get; }
    
    public AgentNotFoundException(Guid id) : base($"Member Id {id} not found.")
    {
        Id = id;
    }
}