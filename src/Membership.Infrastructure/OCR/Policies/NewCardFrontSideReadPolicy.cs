using System.Globalization;
using Membership.Core.Consts;
using Membership.Infrastructure.OCR.Consts;

namespace Membership.Infrastructure.OCR.Policies;

public class NewCardFrontSideReadPolicy : ICardReadPolicy
{
    public bool CanBeApplied(CardSide currentCardSide)
        => currentCardSide == CardSide.NewCardFrontSide();

    public OcrData ReadData(CardSide cardSide, string result)
    {
        var eidNo = "";
        var name = "";
        var dob = "";
        var expiry = "";
        var gender = "";

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
            }

            expiry = MethodExtensionHelper.Right(result.Trim(), 10);

            int firstStringPositionForDob = result.IndexOf(" Date of Birth");

            if (firstStringPositionForDob > 0) 
            {
                dob = result.Substring(firstStringPositionForDob - 10, 10);
            }

            DateTime? dtExpiry = null;
            DateTime? dtDob = null;
        
            if (expiry.Trim().Length == 10)
            {
                dtExpiry = DateParseHelper.PaseAsDateOnly(expiry);
            }
        
            if (dob.Trim().Length == 10)
            {
                dtDob = DateParseHelper.PaseAsDateOnly(dob);
            }
        
            var genderType = gender == "M" ? Gender.Male : Gender.Others;

            if (gender == "F")
            {
                genderType = Gender.Female;
            }
        
            return new OcrData
            {
                IdNumber = eidNo,
                Name = name,
                DateofBirth = dtDob,
                ExpiryDate = dtExpiry,
                CardNumber = null,
                CardType = CardType.New,
                Gender = genderType,
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