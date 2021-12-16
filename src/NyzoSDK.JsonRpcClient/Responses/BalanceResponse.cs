using System.Text.Json.Serialization;

namespace BS.NyzoSDK.JsonRpcClient.Responses;

public record BalanceResponse : IResponse
{
    [JsonPropertyName("list_length")]
    public int ListLength { get; init; }

    public long Balance { get; init; }
}
