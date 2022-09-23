using Membership.Core.Exceptions;

namespace Membership.Application.Exceptions.Memberships;

public class UnauthorizedDownloadAccessException : CustomException
{
    
    public UnauthorizedDownloadAccessException() : base("Unauthorized download access.")
    {
    }
}