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


        int firstStringPositionForEid = result.IndexOf("ID Number ");    
        eidNo = result.Substring(firstStringPositionForEid + 10, 18);    

        int firstStringPositionForName = result.IndexOf("Name:");    
        int secondStringPositionForName = result.IndexOf(":  Nationality:");    
        name = result.Substring(firstStringPositionForName + 5, secondStringPositionForName - (firstStringPositionForName + 5));    
        
        var split = name.Split(":");
        
        if (split.Length > 0)
        {
            name = split[0];
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
        
        return new OcrData
        {
            IdNumber = eidNo,
            Name = name,
            DateofBirth = dtDob,
            ExpiryDate = dtExpiry,
            CardNumber = null,
            CardType = CardType.New
        };
    }
}