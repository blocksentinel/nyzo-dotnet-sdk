﻿using System;
using System.IO;
using BS.NyzoSDK.Common;

namespace BS.NyzoSDK.Util.NyzoString;

public class NyzoStringPrefilledData : INyzoString
{
    public NyzoStringPrefilledData(
        byte[] receiverIdentifier,
        byte[] senderData
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
    }

    public byte[] ReceiverIdentifier { get; }
    public byte[] SenderData { get; }

    public NyzoStringType Type => NyzoStringType.PrefilledData;

    public byte[] ToBytes()
    {
        MemoryStream stream = new();
        using BinaryWriter writer = new(stream);
        writer.Write(ReceiverIdentifier);
        writer.Write((byte)SenderData.Length);
        writer.Write(SenderData);

        return stream.ToArray();
    }

    public static NyzoStringPrefilledData FromBytes(
        byte[] bytes
    )
    {
        MemoryStream stream = new(bytes);
        using BinaryReader reader = new(stream);
        byte[] receiverIdentifier = reader.ReadBytes(FieldByteSize.Identifier);
        int senderDataLength = Math.Min(reader.ReadByte() & 0xff, FieldByteSize.MaximumSenderDataLength);
        byte[] senderData = reader.ReadBytes(senderDataLength);

        return new NyzoStringPrefilledData(receiverIdentifier, senderData);
    }

    public static NyzoStringPrefilledData FromHex(
        string receiverHex,
        string dataHex
    )
    {
        string filteredReceiver = receiverHex.Replace("-", "").Substring(0, 64);
        byte[] receiverBytes = filteredReceiver.ToBytes();
        byte[] dataBytes = dataHex.ToBytes();

        return new NyzoStringPrefilledData(receiverBytes, dataBytes);
    }
}
