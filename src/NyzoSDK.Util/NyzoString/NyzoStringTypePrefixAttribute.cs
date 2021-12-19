using System;

namespace BS.NyzoSDK.Util.NyzoString;

[AttributeUsage(AttributeTargets.Field)]
public class NyzoStringTypePrefixAttribute : Attribute
{
    public NyzoStringTypePrefixAttribute(
        string prefix
    )
    {
        Prefix = prefix;
    }

    public string Prefix { get; }
}
