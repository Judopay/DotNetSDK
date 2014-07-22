using System.Threading.Tasks;
using JudoPayDotNet.Errors;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    internal class Refunds : BaseRefunds, IRefunds
    {
        private const string Createrefundsaddress = "transactions/refunds";
        private const string Validaterefundsaddress = "transactions/refunds/validate";

        protected readonly string ValidateRefundAddress;

        public Refunds(ILog logger, IClient client)
            : this(logger, client, Createrefundsaddress, Validaterefundsaddress)
        {
        }

        public Refunds(ILog logger, IClient client, string createAddress, string validateAddress)
            : base(logger, client, createAddress)
        {
            ValidateRefundAddress = validateAddress;
        }

        public Task<IResult<JudoApiErrorModel>> Validate(RefundModel refund)
        {
            var validationError = Validate<RefundModel, JudoApiErrorModel>(RefundValidator, refund);

            return validationError ?? PostInternal<RefundModel, JudoApiErrorModel>(ValidateRefundAddress, refund);
        }
    }
}
