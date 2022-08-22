using System.Text;

namespace Membership.Infrastructure.OCR;

public static class MethodExtensionHelper
{
    public static string RemoveSpecialCharacters(this string str)
    {
        StringBuilder sb = new StringBuilder();
        foreach (char c in str)
        {
            if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '-' || c == ':' || c == '/' || c == ' ')
            {
                sb.Append(c);
            }
        }
        return sb.ToString();
    }
    
    public static string KeepOnlyAlphaCharacters(this string str)
    {
        StringBuilder sb = new StringBuilder();
        foreach (char c in str)
        {
            if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == ' ')
            {
                sb.Append(c);
            }
        }
        return sb.ToString();
    }
    
    public static string Left(this string str, int length)
    {
        str = (str ?? string.Empty);
        return str.Substring(0, Math.Min(length, str.Length));
    }

    public static string Right(this string str, int length)
    {
        str = (str ?? string.Empty);
        return (str.Length >= length)
            ? str.Substring(str.Length - length, length)
            : str;
    }
}