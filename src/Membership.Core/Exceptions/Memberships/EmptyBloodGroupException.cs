
namespace Membership.Core.Exceptions.Memberships;

public class EmptyBloodGroupException : CustomException
{
    public EmptyBloodGroupException() : base("Blood group cannot be empty.")
    {
    }
}