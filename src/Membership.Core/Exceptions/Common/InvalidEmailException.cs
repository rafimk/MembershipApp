
namespace Membership.Core.Exceptions.Common;

public class InvalidEmailException : CustomException
{
    public string Email { get; }
    public InvalidEmailException(string email) : base($"Invalid email : {email} address.")
    {
        Email = email;
    }
}