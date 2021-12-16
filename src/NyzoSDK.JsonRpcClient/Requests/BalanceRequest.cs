using System.Text.Json.Serialization;

namespace BS.NyzoSDK.JsonRpcClient.Requests;

public record BalanceRequest : RequestBase
{
    public override string Method => "balance";
    public string? Identifier { get; init; }

    [JsonPropertyName("nyzo_string")]
    public string? NyzoString { get; init; }
}
