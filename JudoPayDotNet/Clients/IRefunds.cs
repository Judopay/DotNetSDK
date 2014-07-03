using System.Threading.Tasks;
using JudoPayDotNet.Models;

namespace JudoPayDotNet
{
    public interface IRefunds
    {
        Task<IResult<PaymentReceiptModel>> Create(RefundModel refund);
    }
}