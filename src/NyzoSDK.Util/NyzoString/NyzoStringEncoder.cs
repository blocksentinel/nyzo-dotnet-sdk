using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BS.NyzoSDK.Common;

namespace BS.NyzoSDK.Util.NyzoString;

public class NyzoStringEncoder : INyzoStringEncoder
{
    private const int HeaderLength = 4;

    private static readonly char[] CharacterLookup = ("0123456789" +
                                                      "abcdefghijkmnopqrstuvwxyz" + // all except lowercase "L"
                                                      "ABCDEFGHIJKLMNPQRSTUVWXYZ" + // all except uppercase "o"
                                                      "-.~_")
        .ToCharArray(); // see https://tools.ietf.org/html/rfc3986#section-2.3

    private readonly Dictionary<char, int> _characterToValueMap = new();

    public NyzoStringEncoder()
    {
        for (int i = 0; i < CharacterLookup.Length; i++)
        {
            _characterToValueMap.Add(CharacterLookup[i], i);
        }
    }

    public string Encode(
        INyzoString stringObject
    )
    {
        // Get the prefix array from the type and the content array from the content object.
        byte[] prefixBytes = stringObject.Type.GetPrefixBytes(this);
        byte[] contentBytes = stringObject.ToBytes();

        // Determine the length of the expanded array with the header and the checksum. The header is the type-specific
        // prefix in characters followed by a single byte that indicates the length of the content array (four bytes
        // total). The checksum is a minimum of 4 bytes and a maximum of 6 bytes, widening the expanded array so that
        // its length is divisible by 3.
        int checksumLength = 4 + (3 - (contentBytes.Length + 2) % 3) % 3;

        // Create the array and add the header and the content. The first three bytes turn into the user-readable
        // prefix in the encoded string. The next byte specifies the length of the content array, and it is immediately
        // followed by the content array.
        MemoryStream stream = new();
        using (BinaryWriter writer = new(stream))
        {
            foreach (byte t in prefixBytes)
            {
                writer.Write(t);
            }

            writer.Write((byte)contentBytes.Length);
            writer.Write(contentBytes);

            // Compute the checksum and add the appropriate number of bytes to the end of the array.
            byte[] temp = stream.ToArray();
            byte[] checksum = temp.DoubleSha256();
            writer.Write(checksum.Take(checksumLength).ToArray());
        }

        byte[] expandedArray = stream.ToArray();

        // Build and return the encoded string from the expanded array.
        return EncodedStringForBytes(expandedArray);
    }

    public INyzoString? Decode(
        string encodedString
    )
    {
        INyzoString? result = null;

        try
        {
            // Map characters from the old encoding to the new encoding. A few characters were changed to make Nyzo
            // strings more URL-friendly.
            encodedString = encodedString.Replace('*', '-').Replace('+', '.').Replace('=', '~');

            // Map characters that may be mistyped. Nyzo strings contain neither 'l' nor 'O'.
            encodedString = encodedString.Replace('l', '1').Replace('O', '0');

            // Get the type from the prefix.
            NyzoStringType? type = NyzoStringTypeHelper.ForPrefix(encodedString.Substring(0, 4));

            // If the type is valid, continue.
            if (type != null)
            {
                // Get the array representation of the encoded string.
                byte[] expandedArray = BytesForEncodedString(encodedString);

                // Get the content length from the next byte and calculate the checksum length.
                int contentLength = expandedArray[3] & 0xff;
                int checksumLength = expandedArray.Length - contentLength - 4;

                // Only continue if the checksum length is valid.
                if (checksumLength is >= 4 and <= 6)
                {
                    // Calculate the checksum and compare it to the provided checksum. Only create the result array if
                    // the checksums match.
                    byte[] calculatedChecksum = expandedArray.Take(HeaderLength + contentLength)
                        .ToArray()
                        .DoubleSha256()
                        .Take(checksumLength)
                        .ToArray();
                    byte[] providedChecksum = expandedArray.Skip(expandedArray.Length - checksumLength)
                        .Take(expandedArray.Length)
                        .ToArray();

                    if (calculatedChecksum.SequenceEqual(providedChecksum))
                    {
                        // Get the content array. This is the encoded object with the prefix, length byte, and checksum
                        // removed.
                        byte[] contentBytes = expandedArray.Skip(HeaderLength)
                            .Take(expandedArray.Length - checksumLength - HeaderLength)
                            .ToArray();

                        // Make the object from the content array.
                        result = type switch
                        {
                            NyzoStringType.Micropay => NyzoStringMicropay.FromBytes(contentBytes),
                            NyzoStringType.PrefilledData => NyzoStringPrefilledData.FromBytes(contentBytes),
                            NyzoStringType.PrivateSeed => new NyzoStringPrivateSeed(contentBytes),
                            NyzoStringType.PublicIdentifier => new NyzoStringPublicIdentifier(contentBytes),
                            NyzoStringType.Signature => new NyzoStringSignature(contentBytes),
                            NyzoStringType.Transaction => NyzoStringTransaction.FromBytes(contentBytes),
                            _ => result
                        };
                    }
                }
            }
        }
        catch (Exception)
        {
            // Ignored
        }

        return result;
    }

    public byte[] BytesForEncodedString(
        string encodedString
    )
    {
        int arrayLength = (encodedString.Length * 6 + 7) / 8;
        byte[] array = new byte[arrayLength];

        for (int i = 0; i < arrayLength; i++)
        {
            char leftCharacter = encodedString[i * 8 / 6];
            char rightCharacter = encodedString[i * 8 / 6 + 1];

            if (!_characterToValueMap.TryGetValue(leftCharacter, out int leftValue))
            {
                leftValue = 0;
            }

            if (!_characterToValueMap.TryGetValue(rightCharacter, out int rightValue))
            {
                rightValue = 0;
            }

            int bitOffset = i * 2 % 6;
            array[i] = (byte)((((leftValue << 6) + rightValue) >> (4 - bitOffset)) & 0xff);
        }

        return array;
    }

    public string EncodedStringForBytes(
        byte[] array
    )
    {
        int index = 0;
        int bitOffset = 0;
        StringBuilder encodedString = new();

        while (index < array.Length)
        {
            // Get the current and next byte.
            int leftByte = array[index] & 0xff;
            int rightByte = index < array.Length - 1 ? array[index + 1] & 0xff : 0;

            // Append the character for the next 6 bits in the array.
            int lookupIndex = (((leftByte << 8) + rightByte) >> (10 - bitOffset)) & 0x3f;
            encodedString.Append(CharacterLookup[lookupIndex]);

            // Advance forward 6 bits.
            if (bitOffset == 0)
            {
                bitOffset = 6;
            }
            else
            {
                index++;
                bitOffset -= 2;
            }
        }

        return encodedString.ToString();
    }
}
