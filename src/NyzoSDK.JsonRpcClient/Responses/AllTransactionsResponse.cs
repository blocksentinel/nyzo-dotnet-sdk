using System;
using System.Text.Json.Serialization;

namespace BS.NyzoSDK.JsonRpcClient.Responses;

public record AllTransactionsResponse : IResponse
{
    [JsonPropertyName("all_transactions")]
    public Transaction[] AllTransactions { get; init; } = Array.Empty<Transaction>();

    [JsonPropertyName("transactions_pool_size")]
    public int TransactionsPoolSize { get; init; }
}
