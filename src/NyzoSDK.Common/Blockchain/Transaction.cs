using System;
using System.IO;

namespace BS.NyzoSDK.Common.Blockchain;

public record Transaction
{
    public byte Type { get; set; }
    public long Timestamp { get; set; }
    public long Amount { get; set; }
    public byte[] ReceiverIdentifier { get; set; } = Array.Empty<byte>();
    public long PreviousHashHeight { get; set; }
    public byte[]? PreviousBlockHash { get; set; }
    public byte[] SenderIdentifier { get; set; } = Array.Empty<byte>();
    public byte[] SenderData { get; set; } = Array.Empty<byte>();
    public byte[] Signature { get; set; } = Array.Empty<byte>();

    public byte[] ToBytes(
        bool forSigning
    )
    {
        MemoryStream stream = new();
        using BinaryWriter writer = new(stream);
        writer.Write(Type);
        writer.Write(Timestamp);
        writer.Write(Amount);
        writer.Write(ReceiverIdentifier);
        writer.Write(PreviousHashHeight);
        writer.Write(SenderIdentifier);
        writer.Write((byte)SenderData.Length);
        writer.Write(SenderData);
        writer.Write(Signature);

        return stream.ToArray();
    }

    public static Transaction FromBytes(
        byte[] bytes
    )
    {
        MemoryStream stream = new(bytes);
        using BinaryReader reader = new(stream);
        byte type = reader.ReadByte();
        long timestamp = reader.ReadInt64();
        long amount = reader.ReadInt64();
        byte[] receiverIdentifier = reader.ReadBytes(FieldByteSize.Identifier);
        long previousHashHeight = reader.ReadInt64();
        byte[]? previousBlockHash = null;
        byte[] senderIdentifier = reader.ReadBytes(FieldByteSize.Identifier);
        int senderDataLength = Math.Min(reader.ReadByte() & 0xff, FieldByteSize.MaximumSenderDataLength);
        byte[] senderData = reader.ReadBytes(senderDataLength);
        byte[] signature = reader.ReadBytes(FieldByteSize.Signature);

        return new Transaction
        {
            Type = type,
            Timestamp = timestamp,
            Amount = amount,
            ReceiverIdentifier = receiverIdentifier,
            PreviousHashHeight = previousHashHeight,
            PreviousBlockHash = previousBlockHash,
            SenderIdentifier = senderIdentifier,
            SenderData = senderData,
            Signature = signature
        };
    }
}
