using System.Threading;
using System.Threading.Tasks;

namespace BS.NyzoSDK.JsonRpcClient;

public interface IJsonRpcTransport
{
    Task<JsonRpcResponse<TResponse>> SendAsync<TRequest, TResponse>(
        TRequest request,
        string? id,
        CancellationToken cancellationToken = default
    ) where TRequest : class, IRequest where TResponse : IResponse;
}
