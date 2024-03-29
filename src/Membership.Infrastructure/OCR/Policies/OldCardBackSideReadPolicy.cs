﻿using System.Globalization;
using System.Text.RegularExpressions;
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

        try
        {
                int firstStringPositionForDob = result.IndexOf(" Date of Birth");
            
                // if (firstStringPositionForDob > 0) 
                // {
                //     dob = result.Substring(firstStringPositionForDob - 10, 10);
                // }
                
                var splitedResult = result.Split(" ");
        
                var newDates = splitedResult.Where(x => x.Length == 10 && x.Contains("/")).ToList();
                var newCardNumber = splitedResult.Where(x => x.Length == 9).ToList();
        
                foreach (var cardRow in newCardNumber)
                {
                    var cardRegex = new Regex("^[0-9]+$");
                    if (cardRegex.IsMatch(cardRow))
                    {
                        cardNo = cardRow;
                    }
                }

                int index = 0;
                foreach (var row in newDates)
                {
                    var myRegex = new Regex(@"([0-9]{2})\/([0-9]{2})\/([0-9]{4})", RegexOptions.Compiled);
                    if (myRegex.IsMatch(row) && index == 0)
                    {
                        dob = row;
                    } else if (myRegex.IsMatch(row) && index == 1)
                    {
                        expiry = row;
                    }

                    index++;
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
                var backSideVerifyString = "";
                var backSideVerifyStrings = result.Split("ILARE");

                if (backSideVerifyStrings.Count() > 0)
                {
                    backSideVerifyString = backSideVerifyStrings[1];
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