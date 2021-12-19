using System;
using System.Linq;
using System.Reflection;

namespace BS.NyzoSDK.Util.NyzoString;

public static class NyzoStringTypeExtensions
{
    public static byte[] GetPrefixBytes(
        this NyzoStringType type,
        INyzoStringEncoder encoder
    )
    {
        NyzoStringTypePrefixAttribute? attribute = typeof(NyzoStringType).GetMember(type.ToString())
            .FirstOrDefault(member => member.MemberType == MemberTypes.Field)
            ?.GetCustomAttribute<NyzoStringTypePrefixAttribute>();

        if (attribute == null)
        {
            throw new InvalidOperationException($"No prefix defined for {type}");
        }

        return encoder.BytesForEncodedString(attribute.Prefix);
    }
}
