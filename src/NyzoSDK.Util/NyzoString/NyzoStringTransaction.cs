using System;
using BS.NyzoSDK.Common;
using BS.NyzoSDK.Common.Blockchain;

namespace BS.NyzoSDK.Util.NyzoString;

public class NyzoStringTransaction : INyzoString
{
    public NyzoStringTransaction(
        Transaction transaction
    )
    {
        Transaction = transaction;
    }

    public Transaction Transaction { get; }

    public NyzoStringType Type => NyzoStringType.Transaction;

    public byte[] ToBytes()
    {
        byte[] bytes = Transaction.ToBytes(false);

        return bytes;
    }

    public static NyzoStringTransaction FromBytes(
        byte[] bytes
    )
    {
        Transaction transaction = Transaction.FromBytes(bytes);

        return new NyzoStringTransaction(transaction);
    }

    public static NyzoStringTransaction FromHex(
        string timestampHex,
        string amountHex,
        string receiverHex,
        string previousHashHeightHex,
        string previousBlockHashHex,
        string senderHex,
        string dataHex,
        string signatureHex
    )
    {
        long timestamp = BitConverter.ToInt64(timestampHex.ToBytes(), 0);
        long amount = BitConverter.ToInt64(amountHex.ToBytes(), 0);
        string filteredReceiver = receiverHex.Replace("-", "").Substring(0, 64);
        byte[] receiverBytes = filteredReceiver.ToBytes();
        long previousHashHeight = BitConverter.ToInt64(previousHashHeightHex.ToBytes(), 0);
        string filteredHash = previousBlockHashHex.Replace("-", "").Substring(0, 64);
        byte[] previousBlockHashBytes = filteredHash.ToBytes();
        string filteredSender = senderHex.Replace("-", "").Substring(0, 64);
        byte[] senderBytes = filteredSender.ToBytes();
        byte[] dataBytes = dataHex.ToBytes();
        byte[] signatureBytes = signatureHex.ToBytes();

        return new NyzoStringTransaction(new Transaction
        {
            Type = 2,
            Timestamp = timestamp,
            Amount = amount,
            ReceiverIdentifier = receiverBytes,
            PreviousHashHeight = previousHashHeight,
            PreviousBlockHash = previousBlockHashBytes,
            SenderIdentifier = senderBytes,
            SenderData = dataBytes,
            Signature = signatureBytes
        });
    }
}
