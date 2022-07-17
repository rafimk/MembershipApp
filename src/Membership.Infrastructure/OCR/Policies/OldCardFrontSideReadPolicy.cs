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
        
        int firstStringPositionForEid = result.IndexOf("ID Number ");    
        eidNo = result.Substring(firstStringPositionForEid + 10, 18);   
        
        int firstStringPositionForName = result.IndexOf("Name:");    
        int secondStringPositionForName = result.IndexOf(":  Nationality:");    
        name = result.Substring(firstStringPositionForName + 5, secondStringPositionForName - (firstStringPositionForName + 5));    

        var split = name.Split(":");
        
        if (split.Length > 1)
        {
            name = split[0];
        }
        
        return new OcrData
        {
            IdNumber = eidNo,
            Name = name,
            DateofBirth = null,
            ExpiryDate = null,
            CardNumber = "",
            CardType = CardType.New
        };
    }
}