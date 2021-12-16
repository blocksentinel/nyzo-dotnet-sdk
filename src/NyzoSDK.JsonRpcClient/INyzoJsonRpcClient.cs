using System.Threading;
using System.Threading.Tasks;
using BS.NyzoSDK.JsonRpcClient.Requests;
using BS.NyzoSDK.JsonRpcClient.Responses;

namespace BS.NyzoSDK.JsonRpcClient;

public interface INyzoJsonRpcClient
{
    Task<JsonRpcResponse<EchoResponse>> EchoAsync(
        string? id = null,
        CancellationToken cancellationToken = default
    );

    Task<JsonRpcResponse<InfoResponse>> InfoAsync(
        string? id = null,
        CancellationToken cancellationToken = default
    );

    Task<JsonRpcResponse<BalanceResponse>> BalanceAsync(
        BalanceRequest request,
        string? id = null,
        CancellationToken cancellationToken = default
    );

    Task<JsonRpcResponse<AllTransactionsResponse>> AllTransactionsAsync(
        string? id = null,
        CancellationToken cancellationToken = default
    );

    Task<JsonRpcResponse<GetTransactionResponse>> GetTransactionAsync(
        long height,
        string? id = null,
        CancellationToken cancellationToken = default
    );

    Task<JsonRpcResponse<BroadcastResponse>> BroadcastAsync(
        string transaction,
        string? id = null,
        CancellationToken cancellationToken = default
    );

    Task<JsonRpcResponse<RawTransactionResponse>> RawTransactionAsync(
        RawTransactionRequest rawTransaction,
        string? id = null,
        CancellationToken cancellationToken = default
    );

    Task<JsonRpcResponse<TransactionConfirmedResponse>> TransactionConfirmedAsync(
        string transaction,
        string? id = null,
        CancellationToken cancellationToken = default
    );

    Task<JsonRpcResponse<CycleInfoResponse>> CycleInfoAsync(
        string? id = null,
        CancellationToken cancellationToken = default
    );

    Task<JsonRpcResponse<BlockResponse>> BlockAsync(
        long height,
        string? id = null,
        CancellationToken cancellationToken = default
    );
}
