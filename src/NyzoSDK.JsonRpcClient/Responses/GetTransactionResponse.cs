﻿using System;
using System.Text.Json.Serialization;

namespace BS.NyzoSDK.JsonRpcClient.Responses;

public record GetTransactionResponse : IResponse
{
    [JsonPropertyName("transaction")]
    public JsonRpcTransaction[] Transactions { get; init; } = Array.Empty<JsonRpcTransaction>();
}
