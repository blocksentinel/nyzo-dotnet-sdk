using System.Text.Json.Serialization;

namespace BS.NyzoSDK.JsonRpcClient;

public abstract record RequestBase : IRequest
{
    [JsonIgnore]
    public abstract string Method { get; }
}
