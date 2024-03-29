﻿using System;
using System.IO;
using BS.NyzoSDK.Common;

namespace BS.NyzoSDK.Util.NyzoString;

public class NyzoStringMicropay : INyzoString
{
    public NyzoStringMicropay(
        byte[] receiverIdentifier,
        byte[] senderData,
        long amount,
        long timestamp,
        long previousHashHeight,
        byte[] previousBlockHash
    )
    {
        ReceiverIdentifier = receiverIdentifier;
        if (senderData.Length <= FieldByteSize.MaximumSenderDataLength)
        {
            SenderData = senderData;
        }
        else
        {
            SenderData = new byte[FieldByteSize.MaximumSenderDataLength];
            Array.Copy(senderData, SenderData, FieldByteSize.MaximumSenderDataLength);
        }

        Amount = amount;
        Timestamp = timestamp;
        PreviousHashHeight = previousHashHeight;
        PreviousBlockHash = previousBlockHash;
    }

    public byte[] ReceiverIdentifier { get; }
    public byte[] SenderData { get; }
    public long Amount { get; }
    public long Timestamp { get; }
    public long PreviousHashHeight { get; }
    public byte[] PreviousBlockHash { get; }

    public NyzoStringType Type => NyzoStringType.Micropay;

    public byte[] ToBytes()
    {
        MemoryStream stream = new();
        using BinaryWriter writer = new(stream);
        writer.Write(ReceiverIdentifier);
        writer.Write((byte)SenderData.Length);
        writer.Write(SenderData);
        writer.Write(Amount);
        writer.Write(Timestamp);
        writer.Write(PreviousHashHeight);
        writer.Write(PreviousBlockHash);

        return stream.ToArray();
    }

    public static NyzoStringMicropay FromBytes(
        byte[] bytes
    )
    {
        MemoryStream stream = new(bytes);
        using BinaryReader reader = new(stream);
        byte[] receiverIdentifier = reader.ReadBytes(FieldByteSize.Identifier);
        int senderDataLength = Math.Min(reader.ReadByte() & 0xff, FieldByteSize.MaximumSenderDataLength);
        byte[] senderData = reader.ReadBytes(senderDataLength);
        long amount = reader.ReadInt64();
        long timestamp = reader.ReadInt64();
        long previousHashHeight = reader.ReadInt64();
        byte[] previousBlockHash = reader.ReadBytes(FieldByteSize.Hash);

        return new NyzoStringMicropay(receiverIdentifier, senderData, amount, timestamp, previousHashHeight, previousBlockHash);
    }

    public static NyzoStringMicropay FromHex(
        string receiverHex,
        string dataHex,
        string amountHex,
        string timestampHex,
        string previousHashHeightHex,
        string previousBlockHashHex
    )
    {
        string filteredReceiver = receiverHex.Replace("-", "").Substring(0, 64);
        byte[] receiverBytes = filteredReceiver.ToBytes();
        byte[] dataBytes = dataHex.ToBytes();
        long amount = BitConverter.ToInt64(amountHex.ToBytes(), 0);
        long timestamp = BitConverter.ToInt64(timestampHex.ToBytes(), 0);
        long previousHashHeight = BitConverter.ToInt64(previousHashHeightHex.ToBytes(), 0);
        string filteredHash = previousBlockHashHex.Replace("-", "").Substring(0, 64);
        byte[] previousBlockHashBytes = filteredHash.ToBytes();

        return new NyzoStringMicropay(receiverBytes, dataBytes, amount, timestamp, previousHashHeight, previousBlockHashBytes);
    }
}
