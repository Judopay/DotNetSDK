using System.Threading.Tasks;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients.WebPayments
{
    public interface ITransactions
    {
        Task<IResult<WebPaymentRequestModel>> Get(string reference);
        Task<IResult<WebPaymentRequestModel>> GetByReceipt(string receiptId);
    }
}