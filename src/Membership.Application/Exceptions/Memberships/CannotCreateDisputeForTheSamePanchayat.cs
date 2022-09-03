using Membership.Core.Exceptions;

namespace Membership.Application.Exceptions.Memberships;

public class CannotCreateDisputeForTheSamePanchayat : CustomException
{
    public CannotCreateDisputeForTheSamePanchayat() : base("Cannot create a dispute request for the same panchayat")
    {
    }
}