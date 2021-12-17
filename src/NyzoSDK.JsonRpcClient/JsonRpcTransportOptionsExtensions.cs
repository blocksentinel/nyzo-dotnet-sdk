namespace BS.NyzoSDK.JsonRpcClient;

public static class JsonRpcTransportOptionsExtensions
{
    public static JsonRpcTransportOptions Url(
        this JsonRpcTransportOptions options,
        string url
    )
    {
        options.Url = url;

        return options;
    }

    public static JsonRpcTransportOptions IdGenerator(
        this JsonRpcTransportOptions options,
        IIdGenerator idGenerator
    )
    {
        options.IdGenerator = idGenerator;

        return options;
    }
}
