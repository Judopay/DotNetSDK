using System.Threading.Tasks;
using JudoPayDotNet.Models;

namespace JudoPayDotNet
{
    public interface ITransactions
    {
        Task<IResult<PaymentReceiptModel>> Get(string receiptId);
        Task<IResult<PaymentReceiptResults>> Get(string transactionType, long? pageSize = null, 
                                                    long? offset = null, string sort = null);
    }
}