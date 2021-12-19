using AutoFixture.Xunit2;
using BS.NyzoSDK.Util.NyzoString;
using Shouldly;
using Xunit;

namespace BS.NyzoSDK.Util.Tests.NyzoString;

public class NyzoStringTypeHelperTests
{
    [Theory]
    [InlineAutoData("pay_", NyzoStringType.Micropay)]
    [InlineAutoData("pre_", NyzoStringType.PrefilledData)]
    [InlineAutoData("key_", NyzoStringType.PrivateSeed)]
    [InlineAutoData("id__", NyzoStringType.PublicIdentifier)]
    [InlineAutoData("sig_", NyzoStringType.Signature)]
    [InlineAutoData("tx__", NyzoStringType.Transaction)]
    public void ForPrefix_should_return_valid_enum_type(
        string prefix,
        NyzoStringType type
    )
    {
        // Act
        NyzoStringType? result = NyzoStringTypeHelper.ForPrefix(prefix);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBe(type);
    }
}
