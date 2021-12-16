using System.Text.Json.Serialization;

namespace BS.NyzoSDK.JsonRpcClient;

public record Transaction
{
    public long Amount { get; set; }
    public string Receiver { get; set; } = "";
    public string Signature { get; set; } = "";
    public long Fee { get; set; }

    [JsonPropertyName("type_enum")]
    public string TypeEnum { get; set; } = "";

    [JsonPropertyName("previous_block_hash")]
    public string PreviousBlockHash { get; set; } = "";

    public string Type { get; set; } = "";

    [JsonPropertyName("sender_data")]
    public string SenderData { get; set; } = "";

    [JsonPropertyName("previous_hash_height")]
    public long PreviousHashHeight { get; set; }

    [JsonPropertyName("sender_nyzo_string")]
    public string SenderNyzoString { get; set; } = "";

    public string Sender { get; set; } = "";

    [JsonPropertyName("receiver_nyzo_string")]
    public string ReceiverNyzoString { get; set; } = "";

    public string Id { get; set; } = "";
    public long Timestamp { get; set; }
}
