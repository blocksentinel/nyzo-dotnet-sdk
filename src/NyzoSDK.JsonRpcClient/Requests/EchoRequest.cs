namespace BS.NyzoSDK.JsonRpcClient.Requests;

public record EchoRequest : RequestBase
{
    public override string Method => "echo";
}
