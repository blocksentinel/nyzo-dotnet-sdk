namespace BS.NyzoSDK.JsonRpcClient.Requests;

public record GetTransactionRequest : RequestBase
{
    public override string Method => "gettransaction";
    public long Height { get; init; }
}
