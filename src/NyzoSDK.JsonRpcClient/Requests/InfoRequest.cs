namespace BS.NyzoSDK.JsonRpcClient.Requests;

public record InfoRequest : RequestBase
{
    public override string Method => "info";
}
