using AutoFixture.Xunit2;
using BS.NyzoSDK.Util.NyzoString;
using Shouldly;
using Xunit;

namespace BS.NyzoSDK.Util.UnitTests.NyzoString;

public class NyzoStringPublicIdentifierTests
{
    [Theory]
    [InlineAutoData(Data.Vector0HexString, Data.Vector0PublicIdentifier)]
    [InlineAutoData(Data.Vector8000HexString, Data.Vector8000PublicIdentifier)]
    [InlineAutoData(Data.Vector13000HexString, Data.Vector13000PublicIdentifier)]
    [InlineAutoData(Data.Vector19000HexString, Data.Vector19000PublicIdentifier)]
    [InlineAutoData(Data.Vector25000HexString, Data.Vector25000PublicIdentifier)]
    [InlineAutoData(Data.Vector79000HexString, Data.Vector79000PublicIdentifier)]
    public void Can_encode_and_decode_from_hex(
        string hexString,
        string publicIdentifier,
        NyzoStringEncoder sut
    )
    {
        // Arrange
        NyzoStringPublicIdentifier nyzoString = NyzoStringPublicIdentifier.FromHex(hexString);

        // Act
        string encoded = sut.Encode(nyzoString);
        INyzoString? decoded = sut.Decode(encoded);

        // Assert
        encoded.ShouldBeEquivalentTo(publicIdentifier);
        decoded.ShouldNotBeNull();
        decoded.ToBytes().ShouldBeEquivalentTo(nyzoString.ToBytes());
    }

    internal class Data
    {
        public const string Vector0HexString = "848db2de31cbe4c4-28dbb9e6bdda3aba-98581356ab0e6e02-37b37fd370ac3c7b";
        public const string Vector0PublicIdentifier = "id__88idJKWPQ~j4adLXXIVreIHpn1dnHNXL0AvRw.dNI3PZXtxdHx7u";
        public const string Vector8000HexString = "1a7d496278a9ffc7-febfed9f3e8d83ab-eb4a227d020fbbaa-ab1544f0cef8f53f";
        public const string Vector8000PublicIdentifier = "id__81G.in9WHw_7_I_KERYdxYMIiz9.0x~ZHHJmhf3e~fk_3tn4IuEs";
        public const string Vector13000HexString = "39558c7380ba4817-1a748b48fac7bed0-b2d5cbff5d38bf45-8b1f41aacef67881";
        public const string Vector13000PublicIdentifier = "id__83CmA7e0LBxo6EibifI7MK2QTtM_ojz_hpJwgrIe.Ez1H8dsIm5.";
        public const string Vector19000HexString = "4c66c2c9ef2f6d7a-ec0057d224dcf8eb-f21ce9f1f938fd7c-b3ab6c77ffd805cc";
        public const string Vector19000PublicIdentifier = "id__84PDNJEMbUTYZ01oSzjt~eMQ7eEP~jA.wbeIs7w_U0ocYX2G.tGZ";
        public const string Vector25000HexString = "701a37089b596a18-b719922a543fff1b-54879e7ca1d4fd5c-64eabd2c85d55231";
        public const string Vector25000PublicIdentifier = "id__870rdNzsnnFpKPDiaCg__PKkyXX-Fuj.o6jHMiQ5Tm8PkBAY.aa9";
        public const string Vector79000HexString = "af4fdb9a637d7e83-a0b9d222e3cd1326-8c07c20ad66cf08e-b506f79d2865e2ad";
        public const string Vector79000PublicIdentifier = "id__8a.fUXGAwoY3FbEi8Lfd4Qrc1-8aTDRNAIk6.XSFqvaKuiPwfgjC";
    }
}
