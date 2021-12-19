using System.Linq;
using System.Reflection;

namespace BS.NyzoSDK.Util.NyzoString;

public static class NyzoStringTypeHelper
{
    public static NyzoStringType? ForPrefix(
        string prefix
    )
    {
        FieldInfo? fieldInfo = typeof(NyzoStringType).GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(f => f.GetCustomAttributes<NyzoStringTypePrefixAttribute>().Any())
            .FirstOrDefault(f => f.GetCustomAttribute<NyzoStringTypePrefixAttribute>().Prefix == prefix);

        if (fieldInfo == null)
        {
            return null;
        }

        return (NyzoStringType)fieldInfo.GetValue(null);
    }
}
