using Membership.Core.Exceptions;

namespace Membership.Application.Exceptions.Users;

public class LastFourDigitMismatchException : CustomException
{
    public string LastFourDigit { get; }
    
    public LastFourDigitMismatchException(string lastFourDigit) : base($"User last four digit {lastFourDigit} mismatch.")
    {
        LastFourDigit = lastFourDigit;
    }
}