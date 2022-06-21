using Membership.Core.Exceptions;

namespace Membership.Application.Exceptions.Memberships;

public class EmiratesIdNumberAlreadyInUseException : CustomException
{
    public string Id { get; }
    
    public EmiratesIdNumberAlreadyInUseException(string id) : base($"Member available with this emirates id {id}.")
    {
        Id = id;
    }
}