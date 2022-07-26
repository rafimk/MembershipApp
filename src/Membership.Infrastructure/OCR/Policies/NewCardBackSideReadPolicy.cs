using Membership.Core.Consts;
using Membership.Infrastructure.OCR.Consts;

namespace Membership.Infrastructure.OCR.Policies;

public class NewCardBackSideReadPolicy : ICardReadPolicy
{
    public bool CanBeApplied(CardSide currentCardSide)
        => currentCardSide == CardSide.NewCardBackSide();

    public OcrData ReadData(CardSide cardSide, string result)
    {
        var cardNo = "";
        int firstStringPositionForCardNoStart = result.IndexOf("Card Number");
        int firstStringPositionForCardNoEnd = result.IndexOf(":");
        var firstPart = result.Substring(firstStringPositionForCardNoStart, firstStringPositionForCardNoEnd);

        var split = firstPart.Split(" ");

        if (split.Length > 2)
        {
            cardNo = split[2];
        }

        return new OcrData
        {
            IdNumber = null,
            Name = null,
            DateofBirth = null,
            ExpiryDate = null,
            CardNumber = cardNo,
            CardType = CardType.New,
            Gender = Gender.NotAvailable
        };
    }
}