using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using BS.NyzoSDK.JsonRpcClient;
using BS.NyzoSDK.JsonRpcClient.Requests;
using BS.NyzoSDK.JsonRpcClient.Responses;
using Moq;
using Moq.Protected;
using Shouldly;
using Xunit;

namespace NyzoSDK.JsonRpcClient.UnitTests;

public class JsonRpcTransportTests
{
    [Theory]
    [AutoDomainData]
    public async Task SendAsync_should_throw_JsonRpcException_on_http_request_error(
        [Frozen] Mock<HttpMessageHandler> messageHandlerMock
    )
    {
        // Arrange
        messageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(() => throw new HttpRequestException())
            .Verifiable();
        JsonRpcTransport sut = new(new HttpClient(messageHandlerMock.Object));

        // Act
        JsonRpcException result = await Should.ThrowAsync<JsonRpcException>(
            () => sut.SendAsync<EchoRequest, EchoResponse>(new EchoRequest(), "test"), "Unexpected transport exception caught");

        // Assert
        result.StatusCode.ShouldBeNull();
        result.ReasonPhrase.ShouldBeNull();
        result.InnerException.ShouldNotBeNull();
    }

    [Theory]
    [AutoDomainData]
    public async Task SendAsync_should_throw_JsonRpcException_on_non_success_status_code(
        [Frozen] Mock<HttpMessageHandler> messageHandlerMock
    )
    {
        // Arrange
        messageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(() => new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest })
            .Verifiable();
        JsonRpcTransport sut = new(new HttpClient(messageHandlerMock.Object));

        // Act
        JsonRpcException result = await Should.ThrowAsync<JsonRpcException>(
            () => sut.SendAsync<EchoRequest, EchoResponse>(new EchoRequest(), "test"),
            "Invalid status code returned from server");

        // Assert
        result.StatusCode.ShouldBe(400);
        result.ReasonPhrase.ShouldBe("Bad Request");
        result.InnerException.ShouldBeNull();
    }

    [Theory]
    [AutoDomainData]
    public async Task SendAsync_should_not_throw_when_result_is_null_and_request_is_EchoRequest(
        [Frozen] Mock<HttpMessageHandler> messageHandlerMock
    )
    {
        // Arrange
        messageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(() => new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                    @"{""result"":null,""id"":""82e9d267-7de1-4d2c-88c1-6cfedf831c7f"",""jsonrpc"":""2.0""}")
            })
            .Verifiable();
        JsonRpcTransport sut = new(new HttpClient(messageHandlerMock.Object));

        // Act
        Task<JsonRpcResponse<EchoResponse>> task = sut.SendAsync<EchoRequest, EchoResponse>(new EchoRequest(), "test");
        await Should.NotThrowAsync(() => task);

        // Assert
        task.Result.Result.ShouldBeNull();
    }

    [Theory]
    [AutoDomainData]
    public async Task SendAsync_should_throw_when_result_is_null_and_request_is_not_EchoRequest(
        [Frozen] Mock<HttpMessageHandler> messageHandlerMock
    )
    {
        // Arrange
        messageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(() => new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                    @"{""result"":null,""id"":""82e9d267-7de1-4d2c-88c1-6cfedf831c7f"",""jsonrpc"":""2.0""}")
            })
            .Verifiable();
        JsonRpcTransport sut = new(new HttpClient(messageHandlerMock.Object));

        // Act
        JsonRpcException result = await Should.ThrowAsync<JsonRpcException>(
            () => sut.SendAsync<InfoRequest, InfoResponse>(new InfoRequest(), "test"), "Response was empty");

        // Assert
        result.StatusCode.ShouldBe(200);
        result.ReasonPhrase.ShouldBeNull();
    }

    internal class AutoDomainDataAttribute : AutoDataAttribute
    {
        public AutoDomainDataAttribute() : base(() => new Fixture().Customize(new AutoMoqCustomization())) { }
    }
}
