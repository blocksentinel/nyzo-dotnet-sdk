using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using BS.NyzoSDK.JsonRpcClient.Requests;
using BS.NyzoSDK.JsonRpcClient.Responses;
using Moq;
using Shouldly;
using Xunit;

namespace BS.NyzoSDK.JsonRpcClient.UnitTests;

public class NyzoJsonRpcClientTests
{
    private readonly JsonSerializerOptions _defaultSerializerOptions = new(JsonSerializerDefaults.Web);

    [Theory]
    [AutoDomainData]
    public async Task EchoAsync_should_return_valid_response(
        [Frozen] Mock<IJsonRpcTransport> transportMock,
        NyzoJsonRpcClient sut
    )
    {
        // Arrange
        const string json = @"{""result"":null,""id"":""82e9d267-7de1-4d2c-88c1-6cfedf831c7f"",""jsonrpc"":""2.0""}";
        transportMock
            .Setup(r => r.SendAsync<EchoRequest, EchoResponse>(It.IsAny<EchoRequest>(), It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => JsonSerializer.Deserialize<JsonRpcResponse<EchoResponse>>(json, _defaultSerializerOptions)!);

        // Act
        JsonRpcResponse<EchoResponse> result = await sut.EchoAsync();

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe("82e9d267-7de1-4d2c-88c1-6cfedf831c7f");
    }

    [Theory]
    [AutoDomainData]
    public async Task InfoAsync_should_return_valid_response(
        [Frozen] Mock<IJsonRpcTransport> transportMock,
        NyzoJsonRpcClient sut
    )
    {
        // Arrange
        const string json =
            @"{""result"":{""retention_edge"":-1,""identifier"":""d32fada8dbd4a36f-d6aaddca8a8334f4-498d35e96ea8f8e7-d3c818c31cfe9e6e"",""voting_pool_size"":3112,""trailing_edge"":-1,""nickname"":""d32f...9e6e"",""block_creation_information"":""0\/0"",""cycle_length"":2804,""transaction_pool_size"":0,""version"":606020,""frozen_edge"":14702621,""nyzo_string"":""id__8dcMIrAsTadMTHIuQFH3dfh9AjoGsHAWX.f86cct_GXLwbWyUfna""},""id"":""82e9d267-7de1-4d2c-88c1-6cfedf831c7f"",""jsonrpc"":""2.0""}";
        transportMock
            .Setup(r => r.SendAsync<InfoRequest, InfoResponse>(It.IsAny<InfoRequest>(), It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => JsonSerializer.Deserialize<JsonRpcResponse<InfoResponse>>(json, _defaultSerializerOptions)!);

        // Act
        JsonRpcResponse<InfoResponse> result = await sut.InfoAsync();

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe("82e9d267-7de1-4d2c-88c1-6cfedf831c7f");
        result.Result.ShouldNotBeNull();
        result.Result.RetentionEdge.ShouldBe(-1);
        result.Result.Identifier.ShouldBe("d32fada8dbd4a36f-d6aaddca8a8334f4-498d35e96ea8f8e7-d3c818c31cfe9e6e");
        result.Result.VotingPoolSize.ShouldBe(3112);
        result.Result.TrailingEdge.ShouldBe(-1);
        result.Result.Nickname.ShouldBe("d32f...9e6e");
        result.Result.BlockCreationInformation.ShouldBe("0/0");
        result.Result.CycleLength.ShouldBe(2804);
        result.Result.TrailingEdge.ShouldBe(-1);
        result.Result.Version.ShouldBe(606020);
        result.Result.FrozenEdge.ShouldBe(14702621);
        result.Result.NyzoString.ShouldBe("id__8dcMIrAsTadMTHIuQFH3dfh9AjoGsHAWX.f86cct_GXLwbWyUfna");
        result.Result.TransactionPoolSize.ShouldBe(0);
    }

    [Theory]
    [AutoDomainData]
    public async Task BalanceAsync_should_return_valid_response(
        [Frozen] Mock<IJsonRpcTransport> transportMock,
        NyzoJsonRpcClient sut
    )
    {
        // Arrange
        const string json =
            @"{""result"":{""balance"":8431420,""list_length"":5496},""id"":""82e9d267-7de1-4d2c-88c1-6cfedf831c7f"",""jsonrpc"":""2.0""}";
        transportMock
            .Setup(r => r.SendAsync<BalanceRequest, BalanceResponse>(It.IsAny<BalanceRequest>(), It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => JsonSerializer.Deserialize<JsonRpcResponse<BalanceResponse>>(json, _defaultSerializerOptions)!);

        // Act
        JsonRpcResponse<BalanceResponse> result =
            await sut.BalanceAsync(new BalanceRequest
            {
                NyzoString = "id__8eBf-hvfo4ZYImbhwMrbiVsGtCIFBLfBfUFr716kNCT7K6XZ8PbJ"
            });

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe("82e9d267-7de1-4d2c-88c1-6cfedf831c7f");
        result.Result.ShouldNotBeNull();
        result.Result.Balance.ShouldBe(8431420);
        result.Result.ListLength.ShouldBe(5496);
    }

    [Theory]
    [AutoDomainData]
    public async Task AllTransactionsAsync_should_return_valid_response(
        [Frozen] Mock<IJsonRpcTransport> transportMock,
        NyzoJsonRpcClient sut
    )
    {
        // Arrange
        const string json =
            @"{""result"":{""all_transactions"":[],""transactions_pool_size"":0},""id"":""82e9d267-7de1-4d2c-88c1-6cfedf831c7f"",""jsonrpc"":""2.0""}";
        transportMock
            .Setup(r => r.SendAsync<AllTransactionsRequest, AllTransactionsResponse>(It.IsAny<AllTransactionsRequest>(),
                It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() =>
                JsonSerializer.Deserialize<JsonRpcResponse<AllTransactionsResponse>>(json, _defaultSerializerOptions)!);

        // Act
        JsonRpcResponse<AllTransactionsResponse> result = await sut.AllTransactionsAsync();

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe("82e9d267-7de1-4d2c-88c1-6cfedf831c7f");
        result.Result.ShouldNotBeNull();
        result.Result.AllTransactions.ShouldBeEmpty();
        result.Result.TransactionsPoolSize.ShouldBe(0);
    }

    [Theory]
    [AutoDomainData]
    public async Task GetTransactionAsync_should_return_valid_response(
        [Frozen] Mock<IJsonRpcTransport> transportMock,
        NyzoJsonRpcClient sut
    )
    {
        // Arrange
        const string json =
            @"{""result"":{""transaction"":[]},""id"":""82e9d267-7de1-4d2c-88c1-6cfedf831c7f"",""jsonrpc"":""2.0""}";
        transportMock
            .Setup(r => r.SendAsync<GetTransactionRequest, GetTransactionResponse>(It.IsAny<GetTransactionRequest>(),
                It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() =>
                JsonSerializer.Deserialize<JsonRpcResponse<GetTransactionResponse>>(json, _defaultSerializerOptions)!);

        // Act
        JsonRpcResponse<GetTransactionResponse> result = await sut.GetTransactionAsync(14703450);

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe("82e9d267-7de1-4d2c-88c1-6cfedf831c7f");
        result.Result.ShouldNotBeNull();
        result.Result.Transactions.ShouldBeEmpty();
    }

    [Theory]
    [AutoDomainData]
    public async Task BroadcastAsync_should_return_valid_response(
        [Frozen] Mock<IJsonRpcTransport> transportMock,
        NyzoJsonRpcClient sut
    )
    {
        // Arrange
        const string json =
            @"{""result"":{""target_height"":14704052},""id"":""82e9d267-7de1-4d2c-88c1-6cfedf831c7f"",""jsonrpc"":""2.0""}";
        transportMock
            .Setup(r => r.SendAsync<BroadcastRequest, BroadcastResponse>(It.IsAny<BroadcastRequest>(), It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => JsonSerializer.Deserialize<JsonRpcResponse<BroadcastResponse>>(json, _defaultSerializerOptions)!);

        // Act
        JsonRpcResponse<BroadcastResponse> result = await sut.BroadcastAsync(
            "020000017dc73c7bb20000000000000002daac63af7ba854123d48dc14f3722c87110ff77b9383b75b5c8c50abae62c0960000000000e05daf9526b43eeae5116cf4faf6202acb5710b18f717cc0d1da30bc851d76eb8b799d003cf0a15c3c42e2fba1fb94b8d3ca5be2ac920327a2c43e662d85d22915e2250166a6d2800b61ef6f5e26bde8814652e12ec06859bbe1ef6edfffe1d3f23b0d01");

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe("82e9d267-7de1-4d2c-88c1-6cfedf831c7f");
        result.Result.ShouldNotBeNull();
        result.Result.TargetHeight.ShouldBe(14704052);
    }

    [Theory]
    [AutoDomainData]
    public async Task RawTransactionAsync_should_return_valid_response(
        [Frozen] Mock<IJsonRpcTransport> transportMock,
        NyzoJsonRpcClient sut
    )
    {
        // Arrange
        const string json =
            @"{""result"":{""receiver_identifier"":""daac63af7ba854123d48dc14f3722c87110ff77b9383b75b5c8c50abae62c096"",""amount"":2,""validation_error"":"""",""raw"":""020000017dc73c7bb20000000000000002daac63af7ba854123d48dc14f3722c87110ff77b9383b75b5c8c50abae62c0960000000000e05daf9526b43eeae5116cf4faf6202acb5710b18f717cc0d1da30bc851d76eb8b799d003cf0a15c3c42e2fba1fb94b8d3ca5be2ac920327a2c43e662d85d22915e2250166a6d2800b61ef6f5e26bde8814652e12ec06859bbe1ef6edfffe1d3f23b0d01"",""sender_identifier"":""9526b43eeae5116cf4faf6202acb5710b18f717cc0d1da30bc851d76eb8b799d"",""previous_block_hash"":""6874b4d9f19c76abba6b60a51ed8b787ddb818f81a0f269df8347f2f95f58c1a"",""sender_data"":"""",""valid"":true,""valid_signature"":true,""previous_hash_height"":14704047,""sender_nyzo_string"":""id__89kDK3ZHXh5J.fIU82IbmP2PAV5-Nd7rcbQ57osIzVDuKoUD-2RY"",""receiver_nyzo_string"":""id__8dHJpY.ZH5gifkAt5fdQb8th3_uZBWeVnTQckaLLpJ2nUW5dgdEg"",""sign_data"":""020000017dc73c7bb20000000000000002daac63af7ba854123d48dc14f3722c87110ff77b9383b75b5c8c50abae62c0966874b4d9f19c76abba6b60a51ed8b787ddb818f81a0f269df8347f2f95f58c1a9526b43eeae5116cf4faf6202acb5710b18f717cc0d1da30bc851d76eb8b799d5df6e0e2761359d30a8275058e299fcc0381534545f55cf43e41983f5d4c9456"",""validation_warning"":"""",""id"":""6db940624438d36f-f47e0da8e9ec7de1-2a56e183c0e488bc-ae4bc472515f0d93"",""scheduled_block"":14704052,""timestamp"":1639725169586},""id"":""82e9d267-7de1-4d2c-88c1-6cfedf831c7f"",""jsonrpc"":""2.0""}";
        transportMock
            .Setup(r => r.SendAsync<RawTransactionRequest, RawTransactionResponse>(It.IsAny<RawTransactionRequest>(),
                It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() =>
                JsonSerializer.Deserialize<JsonRpcResponse<RawTransactionResponse>>(json, _defaultSerializerOptions)!);

        // Act
        JsonRpcResponse<RawTransactionResponse> result = await sut.RawTransactionAsync(new RawTransactionRequest
        {
            Timestamp = 1639725169586,
            Amount = 2,
            ReceiverNyzoString = "id__8dHJpY.ZH5gifkAt5fdQb8th3_uZBWeVnTQckaLLpJ2nUW5dgdEg",
            SenderNyzoString = "id__89kDK3ZHXh5J.fIU82IbmP2PAV5-Nd7rcbQ57osIzVDuKoUD-2RY",
            Signature =
                "3cf0a15c3c42e2fba1fb94b8d3ca5be2ac920327a2c43e662d85d22915e2250166a6d2800b61ef6f5e26bde8814652e12ec06859bbe1ef6edfffe1d3f23b0d01",
            Broadcast = true
        });

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe("82e9d267-7de1-4d2c-88c1-6cfedf831c7f");
        result.Result.ShouldNotBeNull();
        result.Result.ReceiverIdentifier.ShouldBe("daac63af7ba854123d48dc14f3722c87110ff77b9383b75b5c8c50abae62c096");
        result.Result.ValidationError.ShouldBeEmpty();
        result.Result.Raw.ShouldBe(
            "020000017dc73c7bb20000000000000002daac63af7ba854123d48dc14f3722c87110ff77b9383b75b5c8c50abae62c0960000000000e05daf9526b43eeae5116cf4faf6202acb5710b18f717cc0d1da30bc851d76eb8b799d003cf0a15c3c42e2fba1fb94b8d3ca5be2ac920327a2c43e662d85d22915e2250166a6d2800b61ef6f5e26bde8814652e12ec06859bbe1ef6edfffe1d3f23b0d01");
        result.Result.SenderIdentifier.ShouldBe("9526b43eeae5116cf4faf6202acb5710b18f717cc0d1da30bc851d76eb8b799d");
        result.Result.PreviousBlockHash.ShouldBe("6874b4d9f19c76abba6b60a51ed8b787ddb818f81a0f269df8347f2f95f58c1a");
        result.Result.SenderData.ShouldBeEmpty();
        result.Result.Valid.ShouldBeTrue();
        result.Result.ValidSignature.ShouldBeTrue();
        result.Result.PreviousHashHeight.ShouldBe(14704047);
        result.Result.SenderNyzoString.ShouldBe("id__89kDK3ZHXh5J.fIU82IbmP2PAV5-Nd7rcbQ57osIzVDuKoUD-2RY");
        result.Result.ReceiverNyzoString.ShouldBe("id__8dHJpY.ZH5gifkAt5fdQb8th3_uZBWeVnTQckaLLpJ2nUW5dgdEg");
        result.Result.SignData.ShouldBe(
            "020000017dc73c7bb20000000000000002daac63af7ba854123d48dc14f3722c87110ff77b9383b75b5c8c50abae62c0966874b4d9f19c76abba6b60a51ed8b787ddb818f81a0f269df8347f2f95f58c1a9526b43eeae5116cf4faf6202acb5710b18f717cc0d1da30bc851d76eb8b799d5df6e0e2761359d30a8275058e299fcc0381534545f55cf43e41983f5d4c9456");
        result.Result.ValidationWarning.ShouldBeEmpty();
        result.Result.Id.ShouldBe("6db940624438d36f-f47e0da8e9ec7de1-2a56e183c0e488bc-ae4bc472515f0d93");
        result.Result.ScheduledBlock.ShouldBe(14704052);
        result.Result.Timestamp.ShouldBe(1639725169586);
        result.Result.Amount.ShouldBe(2);
    }

    [Theory]
    [AutoDomainData]
    public async Task TransactionConfirmedAsync_should_return_valid_response(
        [Frozen] Mock<IJsonRpcTransport> transportMock,
        NyzoJsonRpcClient sut
    )
    {
        // Arrange
        const string json =
            @"{""result"":{""signature"":""3cf0...0d01"",""block"":14704052,""message"":""transaction is proceed in chain!"",""status"":""proceed""},""id"":""82e9d267-7de1-4d2c-88c1-6cfedf831c7f"",""jsonrpc"":""2.0""}";
        transportMock
            .Setup(r => r.SendAsync<TransactionConfirmedRequest, TransactionConfirmedResponse>(
                It.IsAny<TransactionConfirmedRequest>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() =>
                JsonSerializer.Deserialize<JsonRpcResponse<TransactionConfirmedResponse>>(json, _defaultSerializerOptions)!);

        // Act
        JsonRpcResponse<TransactionConfirmedResponse> result = await sut.TransactionConfirmedAsync(
            "020000017dc73c7bb20000000000000002daac63af7ba854123d48dc14f3722c87110ff77b9383b75b5c8c50abae62c0960000000000e05daf9526b43eeae5116cf4faf6202acb5710b18f717cc0d1da30bc851d76eb8b799d003cf0a15c3c42e2fba1fb94b8d3ca5be2ac920327a2c43e662d85d22915e2250166a6d2800b61ef6f5e26bde8814652e12ec06859bbe1ef6edfffe1d3f23b0d01");

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe("82e9d267-7de1-4d2c-88c1-6cfedf831c7f");
        result.Result.ShouldNotBeNull();
        result.Result.Signature.ShouldBe("3cf0...0d01");
        result.Result.Block.ShouldBe(14704052);
        result.Result.Message.ShouldBe("transaction is proceed in chain!");
        result.Result.Status.ShouldBe("proceed");
    }

    [Theory]
    [AutoDomainData]
    public async Task CycleInfoAsync_should_return_valid_response(
        [Frozen] Mock<IJsonRpcTransport> transportMock,
        NyzoJsonRpcClient sut
    )
    {
        // Arrange
        const string json =
            @"{""result"":[{""identifier"":""e90ff1178f5c4efa-ad52d17ef68b4b76-e9725ae892e3e43f-6a1a1c1194c25d47"",""address"":""66.70.190.53"",""is_active"":true,""queue_timestamp"":1639722025131,""nickname"":""bullish2262"",""port_tcp"":9444,""nyzo_string"":""id__8eBf-hvfo4ZYImbhwMrbiVsGtCIFBLfBfUFr716kNCT7K6XZ8PbJ""},{""identifier"":""50cfe69298731ae4-e7c6fc5a2d73fc64-364fdb4cf9c7d3c9-e5c665f24779e007"",""address"":""141.95.55.84"",""is_active"":true,""queue_timestamp"":1639722025131,""nickname"":""Connolly13"",""port_tcp"":9444,""nyzo_string"":""id__853fXGaptPIBX-s-nzTR_6gUj.Kc~twjQvo6qw97vv07W2MVqzQD""},{""identifier"":""e8b83a7f8995bedc-a2791b0731c87fa7-0bd8dba317afaac5-33801063c823067c"",""address"":""95.179.153.213"",""is_active"":true,""queue_timestamp"":1639722025131,""nickname"":""neato"",""port_tcp"":9444,""nyzo_string"":""id__8ezWeE~9CsZtFEBs1R78wYtbUdLA5Y~HPje046f88Nq-DrTVWgxY""},{""identifier"":""8bbb67b042bd17cd-731e363fb9b4df27-27d0a5131b6195c2-db0b8530625e4163"",""address"":""78.47.45.152"",""is_active"":true,""queue_timestamp"":1639722025131,""nickname"":""BiHodl9333"",""port_tcp"":9444,""nyzo_string"":""id__88LZqZ12MhwdtPWUfZDSVQtESakj6U6mNKJbyj1zoB5Au5~MnhfR""},{""identifier"":""7c8541c0abc60168-84fd671e5491ebe6-bb3c8c55411464e0-fd009df7082d3b43"",""address"":""135.181.12.162"",""is_active"":true,""queue_timestamp"":1639722025132,""nickname"":""ttl79"",""port_tcp"":9444,""nyzo_string"":""id__87Q5gt2IPx5FyfTE7CihY~rZf8PmghhBWfS0Ewt8bjK3tUFvffUb""}],""id"":""82e9d267-7de1-4d2c-88c1-6cfedf831c7f"",""jsonrpc"":""2.0""}";
        transportMock
            .Setup(r => r.SendAsync<CycleInfoRequest, CycleInfoResponse>(It.IsAny<CycleInfoRequest>(), It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => JsonSerializer.Deserialize<JsonRpcResponse<CycleInfoResponse>>(json, _defaultSerializerOptions)!);

        // Act
        JsonRpcResponse<CycleInfoResponse> result = await sut.CycleInfoAsync();

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe("82e9d267-7de1-4d2c-88c1-6cfedf831c7f");
        result.Result.ShouldNotBeNull();
        result.Result.Count.ShouldBe(5);
    }

    [Theory]
    [AutoDomainData]
    public async Task BlockAsync_should_return_valid_response(
        [Frozen] Mock<IJsonRpcTransport> transportMock,
        NyzoJsonRpcClient sut
    )
    {
        // Arrange
        const string json =
            @"{""result"":{""start_timestamp"":1639722630000,""verification_timestamp"":1639722638877,""balance_list_hash"":""0bffadffe78044985038266d9cd1fda94e055953803b115bff610d65fed85b21"",""previous_block_hash"":""e2636a5949333fae89a13a52e7bfa707c7f8fd52d3747c1a201eb2f6297f40f1"",""transactions"":[{""amount"":280652514,""receiver"":""12d454a69523f739-eb5eb71c7deb8701-1804df336ae0e2c1-9e0b24a636683e31"",""signature"":""bf53e5aab0d053f02b3d0dee025b9416090468c60a4d16363318a650858cb3be5dcedb259d7e48d8b0614abdab1fea39f020c27ff703cc80f981583be889110e"",""fee"":701632,""type_enum"":""seed"",""previous_block_hash"":""bc4cca2a2a50a229256ae3f5b2b5cd49aa1df1e2d0192726c4bb41cdcea15364"",""type"":""0000000000000001"",""sender_data"":"""",""previous_hash_height"":0,""sender_nyzo_string"":""id__81bkmarm8_tXYTYV77VIyN4p1d-RrL3zNqWb9apUr3WP-ys.NG-H"",""sender"":""12d454a69523f739-eb5eb71c7deb8701-1804df336ae0e2c1-9e0b24a636683e31"",""receiver_nyzo_string"":""id__81bkmarm8_tXYTYV77VIyN4p1d-RrL3zNqWb9apUr3WP-ys.NG-H"",""id"":""bbed27f40dcbd91d-1eeb181117efde24-313222702855ca70-ec0a08da7258435a"",""timestamp"":1639722631000}],""hash"":""5375d970eb9966b1fc0122341d2b5197f02a3f9f5bb4ed4642088d85ec4463af"",""height"":14703690},""id"":""82e9d267-7de1-4d2c-88c1-6cfedf831c7f"",""jsonrpc"":""2.0""}";
        transportMock
            .Setup(r => r.SendAsync<BlockRequest, BlockResponse>(It.IsAny<BlockRequest>(), It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => JsonSerializer.Deserialize<JsonRpcResponse<BlockResponse>>(json, _defaultSerializerOptions)!);

        // Act
        JsonRpcResponse<BlockResponse> result = await sut.BlockAsync(14703690);

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe("82e9d267-7de1-4d2c-88c1-6cfedf831c7f");
        result.Result.ShouldNotBeNull();
        result.Result.StartTimestamp.ShouldBe(1639722630000);
        result.Result.VerificationTimestamp.ShouldBe(1639722638877);
        result.Result.BalanceListHash.ShouldBe("0bffadffe78044985038266d9cd1fda94e055953803b115bff610d65fed85b21");
        result.Result.PreviousBlockHash.ShouldBe("e2636a5949333fae89a13a52e7bfa707c7f8fd52d3747c1a201eb2f6297f40f1");
        result.Result.Hash.ShouldBe("5375d970eb9966b1fc0122341d2b5197f02a3f9f5bb4ed4642088d85ec4463af");
        result.Result.Height.ShouldBe(14703690);
        result.Result.Transactions.ShouldNotBeNull();
        result.Result.Transactions.Length.ShouldBe(1);
    }

    internal class AutoDomainDataAttribute : AutoDataAttribute
    {
        public AutoDomainDataAttribute() : base(() => new Fixture().Customize(new AutoMoqCustomization())) { }
    }
}
