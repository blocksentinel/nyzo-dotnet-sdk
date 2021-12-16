using BS.NyzoSDK.JsonRpcClient;
using Shouldly;
using Xunit;

namespace NyzoSDK.JsonRpcClient.UnitTests;

public class DefaultIdGeneratorTests
{
    [Fact]
    public void Generate_should_return_valid_id()
    {
        // Arrange
        DefaultIdGenerator sut = new();

        // Act
        string result = sut.Generate();

        // Assert
        result.ShouldNotBeNull();
        result.Length.ShouldBe(36);
    }
}
