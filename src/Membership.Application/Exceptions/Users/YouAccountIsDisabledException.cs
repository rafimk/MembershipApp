using Membership.Core.Exceptions;

namespace Membership.Application.Exceptions.Users;

public class YouAccountIsDisabledException : CustomException
{
    public YouAccountIsDisabledException() : base("Your account is disabled. Please contact admin")
    {
    }
}

