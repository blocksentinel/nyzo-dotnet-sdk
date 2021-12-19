using AutoFixture.Xunit2;
using BS.NyzoSDK.Util.NyzoString;
using Shouldly;
using Xunit;

namespace BS.NyzoSDK.Util.Tests.NyzoString;

public class NyzoStringPrivateSeedTests
{
    [Theory]
    [InlineAutoData(Data.Vector0HexString, Data.Vector0PrivateSeed)]
    [InlineAutoData(Data.Vector8000HexString, Data.Vector8000PrivateSeed)]
    [InlineAutoData(Data.Vector13000HexString, Data.Vector13000PrivateSeed)]
    [InlineAutoData(Data.Vector19000HexString, Data.Vector19000PrivateSeed)]
    [InlineAutoData(Data.Vector25000HexString, Data.Vector25000PrivateSeed)]
    [InlineAutoData(Data.Vector79000HexString, Data.Vector79000PrivateSeed)]
    public void Can_encode_and_decode_from_hex(
        string hexString,
        string publicIdentifier,
        NyzoStringEncoder sut
    )
    {
        // Arrange
        NyzoStringPrivateSeed nyzoString = NyzoStringPrivateSeed.FromHex(hexString);

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
        public const string Vector0HexString = "74d84ed425f51e6f-aa9bae140e952601-29d16a73241231dc-6962619b5fbc6e27";
        public const string Vector0PrivateSeed = "key_87jpjKgC.hXMHGLL50Ym9x4GSnGR918PV6CzpqKwM6WEgqRzfABZ";
        public const string Vector8000HexString = "83a2c34eef86da60-e0d26b82a305367b-cf4ed6893ed5d807-0f2fae99a97d77bd";
        public const string Vector8000PrivateSeed = "key_88ezNSZMyKGxWd9IxHc5dEMfjKr9fKop1N-MIGDGwov.tBBqPRDY";
        public const string Vector13000HexString = "e58d51a913e209db-8645d6d78f061309-d2af2ef1ed651788-4ea8d4bc4f678401";
        public const string Vector13000PrivateSeed = "key_8endkrBjWxEsyBonTW-64NEiIQZPZnkoz4YFTbPfqWg1jt28sUiM";
        public const string Vector19000HexString = "c253802154f4aa04-906275b8f922ed86-81cf11d2cac11a92-8dcdf3bee1c5af32";
        public const string Vector19000PrivateSeed = "key_8c9jx25k.aF4B69TLfBzZpr1RP7iQJ4rBFVd-ZZyPr-QQWm3Ivcv";
        public const string Vector25000HexString = "2882cc9feb9e0861-ccb999c8400cf515-49b73fab4cc6c7a8-0cffef201fc2e777";
        public const string Vector25000PrivateSeed = "key_82z2R9_IExyyRbDqQ40c.hm9KR~Ijcs7H0R_ZQ0wNLuV9ieWk_p4";
        public const string Vector79000HexString = "d60987f22773e4c7-7efb079e9900554e-b6efb568de81ec74-f7396efab7f5605d";
        public const string Vector79000PrivateSeed = "key_8dp9y_8Et~j7wMJ7EGB0mkYUZZmFVF7JuftXsMHV.n1upfKfwunh";
    }
}
