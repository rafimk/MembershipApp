namespace Membership.Core.Exceptions.Users;

internal sealed class UserAlreadyExistsException : CustomException
{
    public string Email { get; }

    public UserAlreadyExistsException(string email) : base($"User with email: '{email}' already exists.")
    {
        Email = email;
    }
}