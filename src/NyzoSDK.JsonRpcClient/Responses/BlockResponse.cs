using System;
using System.Text.Json.Serialization;

namespace BS.NyzoSDK.JsonRpcClient.Responses;

public record BlockResponse : IResponse
{
    public long Height { get; init; }

    [JsonPropertyName("start_timestamp")]
    public long StartTimestamp { get; init; }

    [JsonPropertyName("verification_timestamp")]
    public long VerificationTimestamp { get; init; }

    public Transaction[] Transactions { get; init; } = Array.Empty<Transaction>();
    public string Hash { get; init; } = "";

    [JsonPropertyName("balance_list_hash")]
    public string BalanceListHash { get; init; } = "";

    [JsonPropertyName("previous_block_hash")]
    public string PreviousBlockHash { get; init; } = "";
}
