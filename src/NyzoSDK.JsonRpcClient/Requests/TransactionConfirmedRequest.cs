using System.Text.Json.Serialization;

namespace BS.NyzoSDK.JsonRpcClient.Requests;

public record TransactionConfirmedRequest : RequestBase
{
    public override string Method => "transactionconfirmed";

    [JsonPropertyName("tx")]
    public string Transaction { get; init; } = "";
}
