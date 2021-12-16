using System;

namespace BS.NyzoSDK.JsonRpcClient;

public class JsonRpcException : Exception
{
    public JsonRpcException(
        string message,
        int? statusCode,
        string? reasonPhrase,
        Exception? innerException = null
    ) : base(message, innerException)
    {
        StatusCode = statusCode;
        ReasonPhrase = reasonPhrase;
    }

    public int? StatusCode { get; }
    public string? ReasonPhrase { get; }
}
