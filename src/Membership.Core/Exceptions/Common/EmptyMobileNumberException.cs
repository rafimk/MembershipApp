
namespace Membership.Core.Exceptions.Common;

public class EmptyMobileNumberException : CustomException
{
    public EmptyMobileNumberException() : base("Mobile number cannot be empty.")
    {
    }
}