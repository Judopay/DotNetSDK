using System.Threading.Tasks;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients.WebPayments
{
    internal class PreAuths : BasePayments, IPreAuths
    {
        private const string TRANSACTIONTYPE = "preauths";

        public PreAuths(ILog logger, IClient client) : base(logger, client)
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
