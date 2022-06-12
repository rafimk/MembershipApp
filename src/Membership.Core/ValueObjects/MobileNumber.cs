using System.Text.RegularExpressions;
using Membership.Core.Exceptions.Common;

namespace Membership.Core.ValueObjects;

public record MobileNumber
{
    public string Value { get; }

    public MobileNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyMobileNumberException();
        }

        if (!ValidateMobileNumber(value))
        {
            throw new InvalidMobileNumberException();
        }
        
        Value = value;
    }
    
    private bool ValidateMobileNumber(string mobileNumber)  
    {  
        Regex regex = new Regex(@"^([\+]?33[-]?|[0])?[1-9][0-9]{8}$");  
        Match match = regex.Match(mobileNumber); 
        
        return match.Success; 
    }  

    public static implicit operator string(MobileNumber mobileNumber)
        => mobileNumber.Value;
    
    public static implicit operator MobileNumber(string mobileNumber)
        => new(mobileNumber);
}