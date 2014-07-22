using System.Threading.Tasks;
using JudoPayDotNet.Errors;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    internal class Payments : BasePayments, IPayments
    {
        private const string Createaddress = "transactions/payments";
        private const string Validateaddress = "transactions/payments/validate";

        protected readonly string ValidateAddress;

        public Payments(ILog logger, IClient client)
            : this(logger, client, Createaddress, Validateaddress)
        {
        }

        public Payments(ILog logger, IClient client, string createAddress, string validateAddress) : base(logger, client, createAddress)
        {
            ValidateAddress = validateAddress;
        }

        public Task<IResult<JudoApiErrorModel>> Validate(CardPaymentModel cardPayment)
        {
            var validationError = Validate<CardPaymentModel, JudoApiErrorModel>(CardPaymentValidator, cardPayment);

            return validationError ?? PostInternal<CardPaymentModel, JudoApiErrorModel>(ValidateAddress, cardPayment);
        }

        public Task<IResult<JudoApiErrorModel>> Validate(TokenPaymentModel tokenPayment)
        {
            var validationError = Validate<TokenPaymentModel, JudoApiErrorModel>(TokenPaymentValidator, tokenPayment);

            return validationError ?? PostInternal<TokenPaymentModel, JudoApiErrorModel>(ValidateAddress, tokenPayment);
        }
    }
}
