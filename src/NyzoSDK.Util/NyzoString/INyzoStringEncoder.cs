namespace BS.NyzoSDK.Util.NyzoString;

public interface INyzoStringEncoder
{
    string Encode(
        INyzoString stringObject
    );

    INyzoString? Decode(
        string encodedString
    );

    byte[] BytesForEncodedString(
        string encodedString
    );

    string EncodedStringForBytes(
        byte[] array
    );
}
