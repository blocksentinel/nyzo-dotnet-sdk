namespace BS.NyzoSDK.JsonRpcClient.Requests;

public record CycleInfoRequest : RequestBase
{
    public override string Method => "cycleinfo";
}
