using System.Globalization;
using System.Text.RegularExpressions;
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
            var splitedResult = result.Split(" ");
            var newEids = splitedResult.Where(x => x.Length == 18).ToList();
            var newDates = splitedResult.Where(x => x.Length == 10).ToList();
            var myDateRegex = new Regex(@"([0-9]{2})\/([0-9]{2})\/([0-9]{4})", RegexOptions.Compiled);
            var cardNoRegex = new Regex("^784-[0-9]{4}-[0-9]{7}-[0-9]{1}$");

            foreach (var item in newEids)
            {
                if (cardNoRegex.IsMatch(item))
                {
                    eidNo = item;
                }
            }

            int firstStringPositionForName = result.IndexOf("Name:");
            int secondStringPositionForName = result.IndexOf("Nationality:");    
            if (firstStringPositionForName > 0 && secondStringPositionForName > 0)
            {
                name = result.Substring(firstStringPositionForName + 5, secondStringPositionForName - (firstStringPositionForName + 5));
                
                int firstStringContainDateOfBirthInName = name.IndexOf("Date of Birth");

                if (firstStringContainDateOfBirthInName > 0)
                {
                    firstStringContainDateOfBirthInName = result.IndexOf("Date of Birth");
                    int takeFrom = result.IndexOf(eidNo);
                    if (takeFrom > 0 && firstStringContainDateOfBirthInName > 0)
                    {
                        name = result.Substring(takeFrom + 18, firstStringContainDateOfBirthInName - (takeFrom + 18));
                    }
                }
                else
                {
                    var split = name.Split(":");
        
                    if (split.Length > 0)
                    {
                        name = split[0];
                    } 
                }
            }

            expiry = MethodExtensionHelper.Right(result.Trim(), 10);

            if (newDates.Count() > 0)
            {
                if (myDateRegex.IsMatch(newDates[0]))
                {
                    dob = newDates[0];
                }
            }

            int firstStringPositionForDob = result.IndexOf("Date of Birth");

            if (firstStringPositionForDob > 0) 
            {
                var dateOfBirthInName = name.IndexOf("Date of Birth");
                if (dateOfBirthInName > 0)
                {
                    var nameSplit = name.Split("Date of Birth");
        
                    if (nameSplit.Length > 0)
                    {
                        name = nameSplit[0];
                        
                        var nameSplitwithDob = name.Split(dob);
        
                        if (nameSplitwithDob.Length > 0)
                        {
                            name = nameSplitwithDob[0];
                        }
                        
                    }
                }
            }

            DateTime? dtExpiry = null;
            DateTime? dtDob = null;
            
        
            if (expiry.Trim().Length == 10 && myDateRegex.IsMatch(expiry))
            {
                dtExpiry = DateParseHelper.PaseAsDateOnly(expiry);
            }
            else
            {
                if (newDates.Count() == 3)
                {
                    if (myDateRegex.IsMatch(newDates[2]))
                    {
                        dtExpiry = DateParseHelper.PaseAsDateOnly(newDates[2]);
                    }
                }
            }
        
            if (dob.Trim().Length == 10)
            {
                dtDob = DateParseHelper.PaseAsDateOnly(dob.Trim());
            }
        
            var genderType = gender == "M" ? Gender.Male : Gender.Others;

            if (gender == "F")
            {
                genderType = Gender.Female;
            }
            
            int firstStringPositionForDateOfBirtInName = name.IndexOf("Date of Birth");

            if (firstStringPositionForDateOfBirtInName > 0)
            {
                name = name.Replace("Date of Birth", "");
            }

            return new OcrData
            {
                IdNumber = eidNo,
                Name = name.KeepOnlyAlphaCharacters(),
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