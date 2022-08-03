using Membership.Core.Exceptions;

namespace Membership.Infrastructure.DAL.Exceptions;

public class InvalidCardSideException : CustomException
{
    public string CardSide { get; }
    public InvalidCardSideException(string cardSide) : base($"Invalid card side : {cardSide}.")
    {
        CardSide = cardSide;
    }
}