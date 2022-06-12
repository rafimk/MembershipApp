namespace Membership.Core.Exceptions.Common;

public class EmptyGenericIdException : CustomException
{
    public EmptyGenericIdException() : base("ID cannot be empty.")
    {
    }
}
