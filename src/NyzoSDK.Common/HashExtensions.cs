using System;
using System.Security.Cryptography;

namespace BS.NyzoSDK.Common;

public static class HashExtensions
{
    public static byte[] SingleSha256(
        this byte[]? data
    )
    {
        data ??= Array.Empty<byte>();

        using SHA256? hasher = SHA256.Create();
        byte[] hashed = hasher.ComputeHash(data);

        return hashed;
    }

    public static byte[] DoubleSha256(
        this byte[]? data
    )
    {
        data ??= Array.Empty<byte>();

        using SHA256? hasher = SHA256.Create();
        byte[] hashed = hasher.ComputeHash(hasher.ComputeHash(data));

        return hashed;
    }
}
