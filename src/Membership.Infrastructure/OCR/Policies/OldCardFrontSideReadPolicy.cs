using System.Text.RegularExpressions;
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
            var splitedResult = result.Split(" ");
            var newEids = splitedResult.Where(x => x.Length == 18).ToList();

            foreach (var item in newEids)
            {
                var cardNoRegex = new Regex("^784-[0-9]{4}-[0-9]{7}-[0-9]{1}$");
                if (cardNoRegex.IsMatch(item))
                {
                    eidNo = item;
                }
            }

            int firstStringPositionForName = result.IndexOf("Name:");
            int secondStringPositionForName = result.IndexOf("Nationality:");
            int secondStringPositionForNameWithout = result.IndexOf("Nationality"); 
            if (firstStringPositionForName > 0 && secondStringPositionForName > 0)
            {
                name = result.Substring(firstStringPositionForName + 5, secondStringPositionForName - (firstStringPositionForName + 5));
                name = name.Replace(":", "");

                if (name.Trim().Length == 0)
                {
                    int firstStringPositionForNameNew = result.IndexOf(eidNo);
                    int secondStringPositionForNameNew = result.IndexOf("Name"); 
                    name = result.Substring(firstStringPositionForNameNew + 18, secondStringPositionForNameNew - (firstStringPositionForNameNew + 18));
                    name = name.Replace(":", "").Trim();
                }
                
            }
            else if (firstStringPositionForName > 0 && secondStringPositionForNameWithout > 0)
            {
                name = result.Substring(firstStringPositionForName + 5, secondStringPositionForNameWithout - (firstStringPositionForName + 5));
                name = name.Replace(":", "");

                if (name.Trim().Length == 0)
                {
                    int firstStringPositionForNameNew = result.IndexOf(eidNo);
                    int secondStringPositionForNameNew = result.IndexOf("Name"); 
                    name = result.Substring(firstStringPositionForNameNew + 18, secondStringPositionForNameNew - (firstStringPositionForNameNew + 18));
                    name = name.Replace(":", "").Trim();
                }
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