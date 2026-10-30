using System;

public class StringToCharArray
{
    private static readonly char[] cachedArray = new char[50];
    
    public static char[] Convert(string value, out int length)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (value.Length > cachedArray.Length)
        {
            throw new ArgumentException($"The value cannot exceed {cachedArray.Length} characters.", nameof(value));
        }

        value.CopyTo(0, cachedArray, 0, value.Length);
        length = value.Length;
        return cachedArray;
    }
}
