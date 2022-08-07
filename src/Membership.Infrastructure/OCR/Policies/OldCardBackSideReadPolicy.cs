using System.Globalization;
using Membership.Core.Consts;
using Membership.Infrastructure.OCR.Consts;

namespace Membership.Infrastructure.OCR.Policies;

public class OldCardBackSideReadPolicy : ICardReadPolicy
{
    public bool CanBeApplied(CardSide currentCardSide)
        => currentCardSide == CardSide.OldCardBackSide();

    public OcrData ReadData(CardSide cardSide, string result)
    {
        var expiry = "";
        var cardNo = "";
        var dob = "";
        var gender = "";
        
        var splitedResult = result.Split(" ");

        var rowCount = 0;
        foreach (var item in splitedResult)
        {
            rowCount++;
            if (item.ToLower() == "signature")
            {
                if (splitedResult.Count() > (rowCount + 1))
                {
                    var testData1 = splitedResult[rowCount];
                    var testData2 = splitedResult[rowCount + 1];
                    var slashIndex = testData1.IndexOf("/");
                    if (slashIndex > 0)
                    {
                        expiry = testData1;
                        cardNo = testData2;
                    }
                    slashIndex = testData2.IndexOf("/");
                    if (slashIndex > 0)
                    {
                        expiry = testData2;
                        cardNo = testData1;
                    }
                }
            }
        }

        // int firstStringPositionForExpiry = result.IndexOf("Card Number");
        //
        // if (firstStringPositionForExpiry > 0) 
        // {
        //     expiry = result.Substring(firstStringPositionForExpiry + 11, 23);
        //     var expirySplit = expiry.Split(" ");
        //     if (expirySplit.Length > 2)
        //     {
        //         expiry = expirySplit[1];
        //         cardNo = expirySplit[2];
        //     }
        // }
        
        int firstStringPositionForDob = result.IndexOf(" Date of Birth");

        if (firstStringPositionForDob > 0) 
        {
            dob = result.Substring(firstStringPositionForDob - 10, 10);
        }
        
        var genderIndexStart = result.IndexOf("Sex:");
        if (genderIndexStart > 0) 
        {
            gender = result.Substring(genderIndexStart + 5, 1);
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
            IdNumber = null,
            Name = null,
            DateofBirth = dtDob,
            ExpiryDate = dtExpiry,
            CardNumber = cardNo,
            CardType = CardType.Old,
            Gender = genderType
        };
    }
}