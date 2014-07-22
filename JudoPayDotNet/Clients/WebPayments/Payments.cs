using System.Threading.Tasks;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients.WebPayments
{
    internal class Payments : BasePayments, IPayments
    {
        private const string Transactiontype = "payments";

        public Payments(ILog logger, IClient client)
            : base(logger, client)
        {
        }

        public Task<IResult<WebPaymentResponseModel>> Create(WebPaymentRequestModel model)
        {
            return Create(model, Transactiontype);
        }

        public Task<IResult<WebPaymentRequestModel>> Update(WebPaymentRequestModel model)
        {
            return Update(model, Transactiontype);
        }
    }
}
