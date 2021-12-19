namespace BS.NyzoSDK.Util.NyzoString;

public enum NyzoStringType
{
    [NyzoStringTypePrefix("pay_")] Micropay,
    [NyzoStringTypePrefix("pre_")] PrefilledData,
    [NyzoStringTypePrefix("key_")] PrivateSeed,
    [NyzoStringTypePrefix("id__")] PublicIdentifier,
    [NyzoStringTypePrefix("sig_")] Signature,
    [NyzoStringTypePrefix("tx__")] Transaction
}
