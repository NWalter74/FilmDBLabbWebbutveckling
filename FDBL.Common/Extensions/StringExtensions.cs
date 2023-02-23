namespace FDBL.Common.Extensions;

public static class StringExtensions
{
    public static string Truncate(this string value, int length)
    {
        /*a public static string extension method named Truncate with an int parameter for the number of characters to return. It returns an emtpty string if the string parmater is null or 
         *empty, the value as is from the string parameter if the string length is less than or equal to the desired number of characters, and the truncatred string followed by three ellipsis if the 
         *string is longer than the desired numberof characters.*/

        if (string.IsNullOrWhiteSpace(value)) return string.Empty;
        if (value.Length <= length) return value;

        return $"{value[..length]} ...";
    }
}
