using System.Threading.Tasks;
using JudoPayDotNet.Models;

namespace JudoPayDotNet
{
    public interface IPreAuths
    {
        Task<IResult<PaymentReceiptModel>> Create(CardPaymentModel cardPreAuth);
        Task<IResult<PaymentReceiptModel>> Create(TokenPaymentModel tokenPreAuth);
    }
}