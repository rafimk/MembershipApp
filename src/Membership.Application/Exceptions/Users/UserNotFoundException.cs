using Membership.Core.Exceptions;

namespace Membership.Application.Exceptions.Users;

public class UserNotFoundException : CustomException
{
    public Guid? Id { get; }
    
    public UserNotFoundException(Guid? id) : base($"User Id {id} not found.")
    {
        Id = id;
    }
}