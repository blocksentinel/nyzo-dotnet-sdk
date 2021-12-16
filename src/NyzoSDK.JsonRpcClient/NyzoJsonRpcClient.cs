using System.Threading;
using System.Threading.Tasks;
using BS.NyzoSDK.JsonRpcClient.Requests;
using BS.NyzoSDK.JsonRpcClient.Responses;

namespace BS.NyzoSDK.JsonRpcClient;

public class NyzoJsonRpcClient : INyzoJsonRpcClient
{
    private readonly IJsonRpcTransport _transport;

    public NyzoJsonRpcClient(
        IJsonRpcTransport transport
    )
    {
        _transport = transport;
    }

    public Task<JsonRpcResponse<EchoResponse>> EchoAsync(
        string? id = null,
        CancellationToken cancellationToken = default
    )
    {
        return _transport.SendAsync<EchoRequest, EchoResponse>(new EchoRequest(), id, cancellationToken);
    }

    public Task<JsonRpcResponse<InfoResponse>> InfoAsync(
        string? id = null,
        CancellationToken cancellationToken = default
    )
    {
        return _transport.SendAsync<InfoRequest, InfoResponse>(new InfoRequest(), id, cancellationToken);
    }

    public Task<JsonRpcResponse<BalanceResponse>> BalanceAsync(
        BalanceRequest request,
        string? id = null,
        CancellationToken cancellationToken = default
    )
    {
        return _transport.SendAsync<BalanceRequest, BalanceResponse>(request, id, cancellationToken);
    }

    public Task<JsonRpcResponse<AllTransactionsResponse>> AllTransactionsAsync(
        string? id = null,
        CancellationToken cancellationToken = default
    )
    {
        return _transport.SendAsync<AllTransactionsRequest, AllTransactionsResponse>(new AllTransactionsRequest(), id,
            cancellationToken);
    }

    public Task<JsonRpcResponse<GetTransactionResponse>> GetTransactionAsync(
        long height,
        string? id = null,
        CancellationToken cancellationToken = default
    )
    {
        return _transport.SendAsync<GetTransactionRequest, GetTransactionResponse>(new GetTransactionRequest { Height = height },
            id, cancellationToken);
    }

    public Task<JsonRpcResponse<BroadcastResponse>> BroadcastAsync(
        string transaction,
        string? id = null,
        CancellationToken cancellationToken = default
    )
    {
        return _transport.SendAsync<BroadcastRequest, BroadcastResponse>(new BroadcastRequest { Transaction = transaction }, id,
            cancellationToken);
    }

    public Task<JsonRpcResponse<RawTransactionResponse>> RawTransactionAsync(
        RawTransactionRequest rawTransaction,
        string? id = null,
        CancellationToken cancellationToken = default
    )
    {
        return _transport.SendAsync<RawTransactionRequest, RawTransactionResponse>(rawTransaction, id, cancellationToken);
    }

    public Task<JsonRpcResponse<TransactionConfirmedResponse>> TransactionConfirmedAsync(
        string transaction,
        string? id = null,
        CancellationToken cancellationToken = default
    )
    {
        return _transport.SendAsync<TransactionConfirmedRequest, TransactionConfirmedResponse>(
            new TransactionConfirmedRequest { Transaction = transaction }, id, cancellationToken);
    }

    public Task<JsonRpcResponse<CycleInfoResponse>> CycleInfoAsync(
        string? id = null,
        CancellationToken cancellationToken = default
    )
    {
        return _transport.SendAsync<CycleInfoRequest, CycleInfoResponse>(new CycleInfoRequest(), id, cancellationToken);
    }

    public Task<JsonRpcResponse<BlockResponse>> BlockAsync(
        long height,
        string? id = null,
        CancellationToken cancellationToken = default
    )
    {
        return _transport.SendAsync<BlockRequest, BlockResponse>(new BlockRequest { Height = height }, id, cancellationToken);
    }
}
