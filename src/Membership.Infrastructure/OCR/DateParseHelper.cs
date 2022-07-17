namespace Membership.Infrastructure.OCR;

internal static class DateParseHelper
{
    public static DateTime? PaseAsDateOnly(string date)
    {
        if (date.Length != 10)
        {
            return null;
        }

        var year = Convert.ToInt16(date.Substring(6, 4));
        var month = Convert.ToInt16(date.Substring(3, 2));
        var day = Convert.ToInt16(date.Substring(0, 2));

        return new DateTime(year, month, day);
    }
}