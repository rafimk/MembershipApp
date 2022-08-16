using Membership.Core.Exceptions;

namespace Membership.Application.Exceptions.Memberships;

public class InvalidPanchayatException : CustomException
{
    
    public InvalidPanchayatException() : base($"Invalid municipality/panchayat")
    {
    }
}