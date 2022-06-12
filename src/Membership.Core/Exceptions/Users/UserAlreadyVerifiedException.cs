namespace Membership.Core.Exceptions.Users;

internal sealed class UserAlreadyVerifiedException : CustomException
{
    public string Email { get; }

    public UserAlreadyVerifiedException(string email) : base($"User with Email: '{email}' is already verified.")
    {
        Email = email;
    }
}