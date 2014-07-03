using JudoPayDotNet.Models;

namespace JudoPayDotNet
{
    public interface ITransactions
    {
        PaymentReceiptModel Get(string receiptId);
        PaymentReceiptResults Get(string transactionType, long? pageSize, long? offeset, string sort);
    }
}