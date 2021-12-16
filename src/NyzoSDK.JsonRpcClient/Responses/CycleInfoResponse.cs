using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BS.NyzoSDK.JsonRpcClient.Responses;

public class CycleInfoResponse : List<CycleInfoResponse.VerifierInfo>, IResponse
{
    public record VerifierInfo
    {
        public string Identifier { get; init; } = "";
        public string Address { get; init; } = "";

        [JsonPropertyName("is_active")]
        public bool IsActive { get; init; }

        [JsonPropertyName("queue_timestamp")]
        public long QueueTimestamp { get; init; }

        public string Nickname { get; init; } = "";

        [JsonPropertyName("port_tcp")]
        public int PortTcp { get; init; }

        [JsonPropertyName("nyzo_string")]
        public string NyzoString { get; init; } = "";
    }
}
