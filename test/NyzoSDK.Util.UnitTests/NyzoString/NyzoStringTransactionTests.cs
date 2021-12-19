using BS.NyzoSDK.Util.NyzoString;
using Shouldly;
using Xunit;

namespace BS.NyzoSDK.Util.UnitTests.NyzoString;

public class NyzoStringTransactionTests
{
    [Fact]
    public void Can_encode_and_decode_from_hex()
    {
        // Arrange
        NyzoStringTransaction nyzoString = NyzoStringTransaction.FromHex("9bdecd1085b8f5e1", "545d4b257f8def80",
            "3ce4eaf311934276-673752ccb5cf4cac-61eed231d8fcb649-6310887ecf99f6e5", "e6e882dc8cd92291",
            "d5fbaeaeb085b299-eb028094fe472330-5eb9f6427e7c3d38-7ece7edc91fb3983",
            "7695f21b83c22fff-5172d720e6aca180-a017d5af55dbd85f-3b23526794a75872", "360cb35fc71a111e2576cf",
            "6a675732bd20a2c203a925fc62f1d5249b98c128b555472c980f84d9d37fb3452c7211ea448eeed51b7af17785490593a429e97a4f373788a1a768e40d64657c");
        NyzoStringEncoder sut = new();

        // Act
        string encoded = sut.Encode(nyzoString);
        INyzoString? decoded = sut.Decode(encoded);

        // Assert
        encoded.ShouldBeEquivalentTo(
            "tx__GgasVJSgysATWmhuiQm_Av~0fejH-P6jgEqEdTbcKt.cI67LSA7p_bq9pP28wJ~q.LoDY8btAdBzBorm-yL3Nz__kobo8erJFp2x5.nMmuMpoRJAkDvkGTyQ2RpcJT_76y4v9osfrDuocISxFJ83Hio-pM7m99LpNizTmktJD0~4Uud_JSkJty7Hh8ZLThKY-ov5ignjG2EGvB-VdWzyGUAB3nhCwe~GDN_e");
        decoded.ShouldNotBeNull();
        decoded.ToBytes().ShouldBeEquivalentTo(nyzoString.ToBytes());
    }
}
