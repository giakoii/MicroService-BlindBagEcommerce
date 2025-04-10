namespace OpenIdConnect.Utils;

/// <summary>
///  StringUtil
/// </summary>
public class StringUtil
{
    /// <summary>
    ///  Convert the first character of the string to uppercase
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToLowerCase(string value)
    {
        if (value == null) 
            return null;
        if (value.Length <= 0) return string.Empty;

        return char.ToLower(value[0]) + value.Substring(1);
    }
}