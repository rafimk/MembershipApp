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

        try
        {
            int firstStringPositionForCardNoStart = result.IndexOf("Card Number");
            int firstStringPositionForCardNoEnd = result.IndexOf(":");

            if (firstStringPositionForCardNoStart > 0 && firstStringPositionForCardNoEnd > 0)
            {
                var firstPart = result.Substring(firstStringPositionForCardNoStart, firstStringPositionForCardNoEnd);
                var split = firstPart.Split(" ");

                if (split.Length > 2)
                {
                    cardNo = split[2];
                }
            }
            
            return new OcrData
            {
                IdNumber = null,
                Name = null,
                DateofBirth = null,
                ExpiryDate = null,
                CardNumber = cardNo,
                CardType = CardType.New,
                Gender = Gender.NotAvailable,
                ErrorOccurred = false
            };
        }
        catch (Exception e)
        {
            return new OcrData
            {
                IdNumber = null,
                Name = null,
                DateofBirth = null,
                ExpiryDate = null,
                CardNumber = null,
                CardType = CardType.New,
                Gender = Gender.NotAvailable,
                ErrorOccurred = true
            };
        }
    }
}