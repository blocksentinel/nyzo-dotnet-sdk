using System;
using System.Text.Json.Serialization;

namespace BS.NyzoSDK.JsonRpcClient.Responses;

public record AllTransactionsResponse : IResponse
{
    [JsonPropertyName("all_transactions")]
    public JsonRpcTransaction[] AllTransactions { get; init; } = Array.Empty<JsonRpcTransaction>();

    [JsonPropertyName("transactions_pool_size")]
    public int TransactionsPoolSize { get; init; }
}
