using System.Text.Json.Serialization;

namespace BS.NyzoSDK.JsonRpcClient.Requests;

public record BroadcastRequest : RequestBase
{
    public override string Method => "broadcast";

    [JsonPropertyName("tx")]
    public string Transaction { get; init; } = "";
}
