using System.Threading.Tasks;
using JudoPayDotNet.Models;

namespace JudoPayDotNet
{
    public interface ITransactions
    {
        Task<IResult<PaymentReceiptModel>> Get(string receiptId);
        Task<IResult<PaymentReceiptResults>> Get(string transactionType, long? pageSize, long? offeset, string sort);
    }
}