using BS.NyzoSDK.Util.NyzoString;
using Shouldly;
using Xunit;

namespace BS.NyzoSDK.Util.UnitTests.NyzoString;

public class NyzoStringPrefilledDataTests
{
    [Fact]
    public void Can_encode_and_decode_from_hex()
    {
        // Arrange
        NyzoStringPrefilledData nyzoString =
            NyzoStringPrefilledData.FromHex("1a74dff9c1bb47f7-01eabcfd217f2d6e-ab58a6889efaa44b-5bbe06d396596560",
                "82fb2a31c2fdedd9");
        NyzoStringEncoder sut = new();

        // Act
        string encoded = sut.Encode(nyzoString);
        INyzoString? decoded = sut.Decode(encoded);

        // Assert
        encoded.ShouldBeEquivalentTo("pre_ahGSV_E1LSwV0vH-_i5_bnYInar8EMHBiTL~1Kennnmx28bZaA72_vVqroRgFqRe");
        decoded.ShouldNotBeNull();
        decoded.ToBytes().ShouldBeEquivalentTo(nyzoString.ToBytes());
    }
}
