namespace BS.NyzoSDK.JsonRpcClient.Requests;

public record BlockRequest : RequestBase
{
    public override string Method => "block";
    public long Height { get; init; }
}
