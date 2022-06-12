
namespace Membership.Core.Exceptions.Common;

public class InvalidEmailException : CustomException
{
    public InvalidEmailException() : base("Invalid email address.")
    {
    }
}