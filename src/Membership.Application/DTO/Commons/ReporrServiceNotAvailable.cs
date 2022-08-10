using Membership.Core.Exceptions;

namespace Membership.Application.DTO.Commons;

public class ReporrServiceNotAvailable : CustomException
{
    public ReporrServiceNotAvailable() : base($"Reports service is not available, please try later")
    {
    }
}