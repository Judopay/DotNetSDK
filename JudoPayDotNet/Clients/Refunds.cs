using System.Threading.Tasks;
using JudoPayDotNet.Clients;
using JudoPayDotNet.Errors;
using JudoPayDotNet.Http;
using JudoPayDotNet.Models;

namespace JudoPayDotNet
{
    internal class Refunds : JudoPayClient, IRefunds
    {
        private const string CREATEREFUNDSADDRESS = "transactions/refunds";
        private const string VALIDATEREFUNDSADDRESS = "transactions/refunds/validate";

        public Refunds(IClient client) : base(client)
        {
        }

        public Task<IResult<PaymentReceiptModel>> Create(RefundModel refund)
        {
            return PostInternal<RefundModel, PaymentReceiptModel>(CREATEREFUNDSADDRESS, refund);
        }

        public Task<IResult<JudoApiErrorModel>> Validate(RefundModel refund)
        {
            return PostInternal<RefundModel, JudoApiErrorModel>(VALIDATEREFUNDSADDRESS, refund);
        }
    }
}
