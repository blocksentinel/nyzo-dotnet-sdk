using System.Text.Json.Serialization;

namespace BS.NyzoSDK.JsonRpcClient.Responses;

public record RawTransactionResponse : IResponse
{
    public string Signature { get; init; } = "";

    [JsonPropertyName("valid_signature")]
    public bool ValidSignature { get; init; }

    [JsonPropertyName("scheduled_block")]
    public long? ScheduledBlock { get; init; }

    public bool Valid { get; init; }

    [JsonPropertyName("validation_error")]
    public string ValidationError { get; init; } = "";

    [JsonPropertyName("validation_warning")]
    public string ValidationWarning { get; init; } = "";

    public string Id { get; init; } = "";
    public long Amount { get; init; }

    [JsonPropertyName("receiver_identifier")]
    public string ReceiverIdentifier { get; init; } = "";

    [JsonPropertyName("sender_identifier")]
    public string SenderIdentifier { get; init; } = "";

    [JsonPropertyName("sender_nyzo_string")]
    public string SenderNyzoString { get; set; } = "";

    [JsonPropertyName("receiver_nyzo_string")]
    public string ReceiverNyzoString { get; init; } = "";

    public long Timestamp { get; init; }

    [JsonPropertyName("sign_data")]
    public string SignData { get; init; } = "";

    [JsonPropertyName("sender_data")]
    public string SenderData { get; init; } = "";

    [JsonPropertyName("previous_hash_height")]
    public long PreviousHashHeight { get; init; }

    [JsonPropertyName("previous_block_hash")]
    public string PreviousBlockHash { get; init; } = "";

    public string Raw { get; init; } = "";
}
