namespace BS.NyzoSDK.JsonRpcClient;

public record JsonRpcResponse<TResponse> where TResponse : IResponse
{
    public string JsonRpc { get; set; } = "2.0";
    public string? Id { get; set; }
    public TResponse? Result { get; set; }
}
