﻿using Membership.Core.Consts;
using Membership.Infrastructure.OCR.Consts;

namespace Membership.Infrastructure.OCR.Policies;

public class OldCardBackSideChildReadPolicy : ICardReadPolicy
{
    public bool CanBeApplied(CardSide currentCardSide)
        => currentCardSide == CardSide.OldCardBackChildSide();

    public OcrData ReadData(CardSide cardSide, string result)
    {
        var expiry = "";
        var cardNo = "";
        var dob = "";
        var gender = "";

        try
        {
             int firstStringPositionForExpiry = result.IndexOf("Signature");
            
                if (firstStringPositionForExpiry > 0) 
                {
                    expiry = result.Substring(firstStringPositionForExpiry + 11, 23);
                    var expirySplit = expiry.Split(" ");
                    if (expirySplit.Length > 2)
                    {
                        cardNo = expirySplit[0];
                        expiry = expirySplit[1];
                    }
                }
                
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
                CardType = CardType.Old,
                Gender = Gender.NotAvailable,
                ErrorOccurred = true
            };
        }
    }
}