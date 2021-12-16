using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using BS.NyzoSDK.JsonRpcClient.Requests;

namespace BS.NyzoSDK.JsonRpcClient;

public class JsonRpcTransport : IJsonRpcTransport
{
    private readonly HttpClient _httpClient;
    private readonly IIdGenerator _idGenerator;
    private readonly Uri _url;

    public JsonRpcTransport(
        HttpClient httpClient,
        IIdGenerator idGenerator
    )
    {
        _httpClient = httpClient;
        _idGenerator = idGenerator;
        _url = new Uri("http://46.101.134.30:4000/jsonrpc");
    }

    public virtual async Task<JsonRpcResponse<TResponse>> SendAsync<TRequest, TResponse>(
        TRequest request,
        string? id,
        CancellationToken cancellationToken = default
    ) where TRequest : class, IRequest where TResponse : IResponse
    {
        try
        {
            HttpResponseMessage result = await _httpClient.PostAsJsonAsync(_url,
                JsonRpcRequest<TRequest>.Create(request, id ?? _idGenerator.Generate()), cancellationToken);

            if (!result.IsSuccessStatusCode)
            {
                throw new JsonRpcException("Invalid status code returned from server", (int)result.StatusCode,
                    result.ReasonPhrase);
            }

            JsonRpcResponse<TResponse>? response =
                await result.Content.ReadFromJsonAsync<JsonRpcResponse<TResponse>>(cancellationToken: cancellationToken);

            if (response == null || response.Result == null && request is not EchoRequest)
            {
                throw new JsonRpcException("Response was empty", (int)result.StatusCode, null);
            }

            return response;
        }
        catch (HttpRequestException e)
        {
            throw new JsonRpcException("Unexpected transport exception caught", null, null, e);
        }
    }
}
