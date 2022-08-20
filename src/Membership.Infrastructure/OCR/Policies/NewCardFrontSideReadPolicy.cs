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
            if (firstStringPositionForName > 0 && secondStringPositionForName > 0)
            {
                name = result.Substring(firstStringPositionForName + 5, secondStringPositionForName - (firstStringPositionForName + 5));
                
                var split = name.Split(":");
        
                if (split.Length > 0)
                {
                    name = split[0];
                }
            }

            expiry = MethodExtensionHelper.Right(result.Trim(), 10);

            if (newDates.Count() > 0)
            {
                var myRegex = new Regex(@"([0-9]{2})\/([0-9]{2})\/([0-9]{4})", RegexOptions.Compiled);
                if (myRegex.IsMatch(newDates[0]))
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
        
            if (expiry.Trim().Length == 10)
            {
                dtExpiry = DateParseHelper.PaseAsDateOnly(expiry);
            }
            else
            {
                if (newDates.Count() == 3)
                {
                    var myRegex = new Regex(@"([0-9]{2})\/([0-9]{2})\/([0-9]{4})", RegexOptions.Compiled);
                    if (myRegex.IsMatch(newDates[2]))
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