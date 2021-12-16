using System.Text.Json.Serialization;

namespace BS.NyzoSDK.JsonRpcClient.Responses;

public record InfoResponse : IResponse
{
    public string Nickname { get; init; } = "";

    [JsonPropertyName("block_creation_information")]
    public string BlockCreationInformation { get; init; } = "";

    [JsonPropertyName("frozen_edge")]
    public long FrozenEdge { get; init; }

    [JsonPropertyName("trailing_edge")]
    public long TrailingEdge { get; init; }

    [JsonPropertyName("retention_edge")]
    public long RetentionEdge { get; init; }

    [JsonPropertyName("cycle_length")]
    public int CycleLength { get; init; }

    public string Identifier { get; init; } = "";

    [JsonPropertyName("nyzo_string")]
    public string NyzoString { get; init; } = "";

    [JsonPropertyName("transaction_pool_size")]
    public int TransactionPoolSize { get; init; }

    [JsonPropertyName("voting_pool_size")]
    public int VotingPoolSize { get; init; }

    public int Version { get; init; }
}
