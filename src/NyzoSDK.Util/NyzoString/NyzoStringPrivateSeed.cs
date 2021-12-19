using BS.NyzoSDK.Common;

namespace BS.NyzoSDK.Util.NyzoString;

public class NyzoStringPrivateSeed : INyzoString
{
    public NyzoStringPrivateSeed(
        byte[] seed
    )
    {
        Seed = seed;
    }

    public byte[] Seed { get; }

    public NyzoStringType Type => NyzoStringType.PrivateSeed;

    public byte[] ToBytes()
    {
        return Seed;
    }

    public static NyzoStringPrivateSeed FromHex(
        string hexString
    )
    {
        string filteredString = hexString.Replace("-", "");
        byte[] bytes = filteredString.ToBytes();

        return new NyzoStringPrivateSeed(bytes);
    }
}
