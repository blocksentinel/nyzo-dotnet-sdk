using System.Text.Json.Serialization;

namespace BS.NyzoSDK.JsonRpcClient.Responses;

public record BroadcastResponse : IResponse
{
    [JsonPropertyName("target_height")]
    public long TargetHeight { get; init; }
}
