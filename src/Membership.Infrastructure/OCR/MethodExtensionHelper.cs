using System.Text;

namespace Membership.Infrastructure.OCR;

public static class MethodExtensionHelper
{
    public static string RemoveSpecialCharacters(this string str)
    {
        StringBuilder sb = new StringBuilder();
        foreach (char c in str)
        {
            if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '-' || c == ':' || c == ' ')
            {
                sb.Append(c);
            }
        }
        return sb.ToString();
    }
}