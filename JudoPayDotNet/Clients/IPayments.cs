using System.Threading.Tasks;
using JudoPayDotNet.Models;

namespace JudoPayDotNet
{
    public interface IPayments
    {
        Task<IResult<PaymentReceiptModel>> Create(CardPaymentModel cardPayment);
        Task<IResult<PaymentReceiptModel>> Create(TokenPaymentModel tokenPayment);
    }
}