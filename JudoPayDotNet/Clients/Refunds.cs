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

        private readonly string _validateRefundAddress;

        public Refunds(ILog logger, IClient client, 
                        string createAddress = Createrefundsaddress, 
                        string validateAddress = Validaterefundsaddress)
            : base(logger, client, createAddress)
        {
            _validateRefundAddress = validateAddress;
        }

        public Task<IResult<JudoApiErrorModel>> Validate(RefundModel refund)
        {
            var validationError = Validate<RefundModel, JudoApiErrorModel>(RefundValidator, refund);

            return validationError ?? PostInternal<RefundModel, JudoApiErrorModel>(_validateRefundAddress, refund);
        }
    }
}
