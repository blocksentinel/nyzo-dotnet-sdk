namespace BS.NyzoSDK.JsonRpcClient.Requests;

public record AllTransactionsRequest : RequestBase
{
    public override string Method => "alltransactions";
}
