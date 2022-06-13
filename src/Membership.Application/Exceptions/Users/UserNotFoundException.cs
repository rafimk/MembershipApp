namespace Membership.Application.Exceptions.Users;

public class UserNotFoundException : MMSException
{
    public Guid Id { get; }
    
    public UserNotFoundException(Guid id) : base($"User Id {id} not found.")
    {
        Id = id;
    }
}