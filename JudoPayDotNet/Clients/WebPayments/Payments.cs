using System.Threading.Tasks;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients.WebPayments
{
    internal class Payments : BasePayments, IPayments
    {
        private const string TRANSACTIONTYPE = "payments";

        public Payments(ILog logger, IClient client)
            : base(logger, client)
        {
        }

        public Task<IResult<WebPaymentResponseModel>> Create(WebPaymentRequestModel model)
        {
            return base.Create(model, TRANSACTIONTYPE);
        }

        public Task<IResult<WebPaymentRequestModel>> Update(WebPaymentRequestModel model)
        {
            return base.Update(model, TRANSACTIONTYPE);
        }
    }
}
