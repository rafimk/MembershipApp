using Membership.Core.Exceptions;

namespace Membership.Infrastructure.Exceptions;

public class NoCardPolicyFoundException : CustomException
{
    public string CardSide { get; }
    
    public NoCardPolicyFoundException(string cardSide) : base($"No card side policy found for : {cardSide}.")
    {
        CardSide = cardSide;
    }
}