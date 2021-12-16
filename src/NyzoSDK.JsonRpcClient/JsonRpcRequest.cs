using System.Text.Json.Serialization;

namespace BS.NyzoSDK.JsonRpcClient;

public record JsonRpcRequest<TRequest> where TRequest : IRequest
{
    [JsonPropertyName("jsonrpc")]
    [JsonPropertyOrder(0)]
    public string JsonRpc => "2.0";

    [JsonPropertyName("method")]
    [JsonPropertyOrder(1)]
    public string Method { get; set; } = "";

    [JsonPropertyName("params")]
    [JsonPropertyOrder(2)]
    public TRequest? Params { get; set; }

    [JsonPropertyName("id")]
    [JsonPropertyOrder(3)]
    public string Id { get; set; } = "";

    public static JsonRpcRequest<TRequest> Create(
        TRequest request,
        string id
    )
    {
        return new JsonRpcRequest<TRequest> { Method = request.Method, Params = request, Id = id };
    }
}
