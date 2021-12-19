using System;
using System.IO;
using System.Linq;

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
        using (BinaryWriter writer = new(stream))
        {
            writer.Write(Type);
            writer.Write(Timestamp);
            writer.Write(Amount);
            writer.Write(ReceiverIdentifier);
            writer.Write(PreviousHashHeight);
            writer.Write(SenderIdentifier);
            writer.Write((byte)SenderData.Length);
            writer.Write(SenderData);
            writer.Write(Signature);
        }

        byte[] bytes = stream.ToArray();

        return bytes;
    }

    public static Transaction FromBytes(
        byte[] bytes
    )
    {
        int index = 0;
        byte type = bytes[index];
        index += FieldByteSize.TransactionType;
        long timestamp = BitConverter.ToInt64(bytes.Skip(index).Take(FieldByteSize.Timestamp).ToArray(), 0);
        index += FieldByteSize.Timestamp;
        long amount = BitConverter.ToInt64(bytes.Skip(index).Take(FieldByteSize.TransactionAmount).ToArray(), 0);
        index += FieldByteSize.TransactionAmount;
        byte[] receiverIdentifier = bytes.Skip(index).Take(FieldByteSize.Identifier).ToArray();
        index += FieldByteSize.Identifier;
        long previousHashHeight = BitConverter.ToInt64(bytes.Skip(index).Take(FieldByteSize.BlockHeight).ToArray(), 0);
        index += FieldByteSize.BlockHeight;
        byte[]? previousBlockHash = null;
        byte[] senderIdentifier = bytes.Skip(index).Take(FieldByteSize.Identifier).ToArray();
        index += FieldByteSize.Identifier;
        int senderDataLength = Math.Min(bytes[index] & 0xff, FieldByteSize.MaximumSenderDataLength);
        index += FieldByteSize.UnnamedByte;
        byte[] senderData = bytes.Skip(index).Take(senderDataLength).ToArray();
        index += senderDataLength;
        byte[] signature = bytes.Skip(index).Take(FieldByteSize.Signature).ToArray();

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
