# Nyzo .NET SDK

> The code found in this repository is unaudited and incomplete. Block Sentinel is not responsible for any losses 
> incurred when using this code.

![GitHub](https://img.shields.io/github/license/blocksentinel/nyzo-dotnet-sdk)

## Features

- JSONRPC Client
- NyzoString

### JSONRPC Client

The JSONRPC Client is a slim wrapper around a standard `HttpClient` that allows for easy communication with the Nyzo 
network. This package is meant to be used in conjunction with an Open Nyzo JSONRPC instance. More information regarding
this can be found on the [Open Nyzo GitHub](https://github.com/Open-Nyzo/nyzoVerifier/issues/5).

#### Using the client

The `NyzoJsonRpcClient` is simple and unassuming. It can be used with, or without dependency injection. See below for a
quick start for using the client.

```cs
var httpClient = new HttpClient();
var transport = new JsonRpcTransport(httpClient);
var client = new NyzoJsonRpcClient(transport);

// Get health information from the JSONRPC node
var response = await client.InfoAsync();

// response.Id: A correlation ID for the request, an optional parameter that can be provided to all requests
// response.Result: A strongly-typed representation of the response from the JSONRPC request

Console.WriteLine($"Server nickname: {response.Result.Nickname}");
// Output:
// Server nickname: d32f...9e6e
```

The `JsonRpcTransport` class has an optional configuration builder that allows for:

- Using a custom `IIdGenerator` for request correlation, the default implementation uses `Guid`
- Overriding the JSONRPC endpoint, the default is `http://127.0.0.1:4000/jsonrpc`

### NyzoString

The NyzoString encoder, as part of the SDK's utility package, enables encoding and decoding of Nyzo-specific strings
for use with reading/creating transations, public identifiers, private keys, etc. More information regarding this can
be found in the [Nyzo Documentation](https://tech.nyzo.co/dataFormats).

#### Using the encoder

The `NyzoStringEncoder` is simple and unassuming. It can be used with, or without dependency injection. See below for a
quick start for using the encoder.

```cs
var encoder = new NyzoStringEncoder();

var nyzoString = NyzoStringPublicIdentifier.FromHex("848db2de31cbe4c4-28dbb9e6bdda3aba-98581356ab0e6e02-37b37fd370ac3c7b");
var encoded = encoder.Encode(nyzoString);

Console.WriteLine($"Encoded Public Identifier: {encoded}");
// Output:
// Encoded Public Identifier: id__88idJKWPQ~j4adLXXIVreIHpn1dnHNXL0AvRw.dNI3PZXtxdHx7u
```

See the `NyzoSDK.Util.UnitTests` project for more examples.

## Resources

* [Open Nyzo](https://github.com/Open-Nyzo/nyzoVerifier)
* [Nyzo](https://github.com/n-y-z-o/nyzoVerifier)
* [NyzoStrings](https://github.com/AngainorDev/NyzoStrings)

## Contributing to this project

Anyone and everyone is welcome to contribute. Please take a moment to
review the [guidelines for contributing](CONTRIBUTING.md).

* [Bug reports](CONTRIBUTING.md#bug-reports)
* [Feature requests](CONTRIBUTING.md#feature-requests)
* [Pull requests](CONTRIBUTING.md#pull-requests)

## License

This project is released under the terms of the Apache License, Version 2.0. See [LICENSE](LICENSE) 
for more information or see https://opensource.org/licenses/Apache-2.0.
