using System;

namespace BS.NyzoSDK.JsonRpcClient;

public class DefaultIdGenerator : IIdGenerator
{
    public string Generate()
    {
        return Guid.NewGuid().ToString("D");
    }
}
