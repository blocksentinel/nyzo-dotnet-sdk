namespace BS.NyzoSDK.Util.NyzoString;

public interface INyzoString
{
    NyzoStringType Type { get; }
    public byte[] ToBytes();
}
