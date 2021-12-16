using System.Text.Json.Serialization;

namespace BS.NyzoSDK.JsonRpcClient.Requests;

public record RawTransactionRequest : RequestBase
{
    public override string Method => "rawtransaction";

    [JsonPropertyName("receiver_identifier")]
    public string? ReceiverIdentifier { get; init; }

    [JsonPropertyName("sender_identifier")]
    public string? SenderIdentifier { get; init; }

    [JsonPropertyName("receiver_nyzo_string")]
    public string? ReceiverNyzoString { get; init; }

    [JsonPropertyName("sender_nyzo_string")]
    public string? SenderNyzoString { get; init; }

    [JsonPropertyName("sender_data")]
    public string SenderData { get; init; } = "";

    public long? Timestamp { get; init; }
    public long Amount { get; init; }

    [JsonPropertyName("previous_hash_height")]
    public long? PreviousHashHeight { get; init; }

    [JsonPropertyName("previous_block_hash")]
    public string? PreviousBlockHash { get; init; }

    public string? Signature { get; init; }

    [JsonPropertyName("private_seed")]
    public string? PrivateSeed { get; init; }

    [JsonPropertyName("private_nyzo_string")]
    public string? PrivateNyzoString { get; init; }

    public bool Broadcast { get; init; }
}
