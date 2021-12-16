namespace BS.NyzoSDK.JsonRpcClient.Responses;

public record TransactionConfirmedResponse : IResponse
{
    public string Message { get; init; } = "";
    public string Status { get; init; } = "";
    public long Block { get; init; }
    public string Signature { get; init; } = "";
}
