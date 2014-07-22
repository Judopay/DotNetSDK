using System.Threading.Tasks;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients.WebPayments
{
    public interface IPayments
    {
        Task<IResult<WebPaymentResponseModel>> Create(WebPaymentRequestModel model);
        Task<IResult<WebPaymentRequestModel>> Update(WebPaymentRequestModel model);
    }
}