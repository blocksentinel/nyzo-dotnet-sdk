using BS.NyzoSDK.Common;

namespace BS.NyzoSDK.Util.NyzoString;

public class NyzoStringPublicIdentifier : INyzoString
{
    public NyzoStringPublicIdentifier(
        byte[] identifier
    )
    {
        Identifier = identifier;
    }

    public byte[] Identifier { get; }

    public NyzoStringType Type => NyzoStringType.PublicIdentifier;

    public byte[] ToBytes()
    {
        return Identifier;
    }

    public static NyzoStringPublicIdentifier FromHex(
        string hexString
    )
    {
        string filteredString = hexString.Replace("-", "");
        byte[] bytes = filteredString.ToBytes();

        return new NyzoStringPublicIdentifier(bytes);
    }
}
