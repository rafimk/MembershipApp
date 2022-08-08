using Membership.Core.Consts;
using Membership.Infrastructure.OCR.Consts;

namespace Membership.Infrastructure.OCR.Policies;

public class OldCardFrontSideReadPolicy : ICardReadPolicy
{
    public bool CanBeApplied(CardSide currentCardSide)
        => currentCardSide == CardSide.OldCardFrontSide();

    public OcrData ReadData(CardSide cardSide, string result)
    {
        string eidNo = "";
        string name = "";

        try
        {
            int firstStringPositionForEid = result.IndexOf("ID Number ");

            if (firstStringPositionForEid > 0)
            {
                eidNo = result.Substring(firstStringPositionForEid + 10, 18);
            }

            int firstStringPositionForName = result.IndexOf("Name:");
            int secondStringPositionForName = result.IndexOf("Nationality:");    
            if (firstStringPositionForName > 0 && secondStringPositionForName > 0)
            {
                name = result.Substring(firstStringPositionForName + 5, secondStringPositionForName - (firstStringPositionForName + 5));
                name = name.Replace(":", "");

                // if (name.Trim().Length == 0)
                // {
                //     int firstStringPositionForNameNew = result.IndexOf(eidNo);
                //     int secondStringPositionForNameNew = result.IndexOf("Name"); 
                //     name = result.Substring(firstStringPositionForNameNew + 18, secondStringPositionForNameNew - (firstStringPositionForNameNew + 18));
                //     name = name.Replace(":", "").Trim();
                // }
                
            }

            return new OcrData
            {
                IdNumber = eidNo,
                Name = name,
                DateofBirth = null,
                ExpiryDate = null,
                CardNumber = "",
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
                CardNumber = "",
                CardType = CardType.New,
                Gender = Gender.NotAvailable,
                ErrorOccurred = true
            };
        }
    }
}