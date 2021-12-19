using AutoFixture.Xunit2;
using BS.NyzoSDK.Util.NyzoString;
using Shouldly;
using Xunit;

namespace BS.NyzoSDK.Util.UnitTests.NyzoString;

public class NyzoStringTypeExtensionsTests
{
    [Theory]
    [InlineAutoData(new byte[] { 96, 168, 127 }, NyzoStringType.Micropay)]
    [InlineAutoData(new byte[] { 97, 163, 191 }, NyzoStringType.PrefilledData)]
    [InlineAutoData(new byte[] { 80, 232, 127 }, NyzoStringType.PrivateSeed)]
    [InlineAutoData(new byte[] { 72, 223, 255 }, NyzoStringType.PublicIdentifier)]
    [InlineAutoData(new byte[] { 109, 36, 63 }, NyzoStringType.Signature)]
    [InlineAutoData(new byte[] { 114, 15, 255 }, NyzoStringType.Transaction)]
    public void GetPrefixBytes_should_return_valid_enum_type(
        byte[] prefixBytes,
        NyzoStringType type
    )
    {
        // Act
        byte[] result = type.GetPrefixBytes(new NyzoStringEncoder());

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBe(prefixBytes);
    }
}
