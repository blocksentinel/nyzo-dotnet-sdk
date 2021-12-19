namespace BS.NyzoSDK.Util.NyzoString;

public class NyzoStringSignature : INyzoString
{
    public NyzoStringSignature(
        byte[] signature
    )
    {
        Signature = signature;
    }

    public byte[] Signature { get; }

    public NyzoStringType Type => NyzoStringType.Signature;

    public byte[] ToBytes()
    {
        return Signature;
    }
}
