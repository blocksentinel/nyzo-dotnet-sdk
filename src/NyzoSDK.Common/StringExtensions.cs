using System;

namespace BS.NyzoSDK.Common;

public static class StringExtensions
{
    public static byte[] ToBytes(
        this string hexString
    )
    {
        int length = hexString.Length;
        byte[] bytes = new byte[length / 2];

        for (int i = 0; i < length; i += 2)
        {
            bytes[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
        }

        return bytes;
    }
}
