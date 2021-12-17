namespace BS.NyzoSDK.JsonRpcClient;

public class JsonRpcTransportOptions
{
    public IIdGenerator? IdGenerator { get; set; }
    public string Url { get; set; } = "http://127.0.0.1:4000/jsonrpc";
}
