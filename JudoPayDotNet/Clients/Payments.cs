using System.Threading.Tasks;
using JudoPayDotNet.Errors;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    internal class Payments : BasePayments, IPayments
    {
        private const string CREATE_ADDRESS = "transactions/payments";
        private const string VALIDATE_ADDRESS = "transactions/payments/validate";

        private readonly string _validateAddress;

        public Payments(ILog logger, IClient client, 
                            string createAddress = CREATE_ADDRESS, 
                            string validateAddress = VALIDATE_ADDRESS) : base(logger, client, createAddress)
        {
            _validateAddress = validateAddress;
        }

        public Task<IResult<JudoApiErrorModel>> Validate(CardPaymentModel cardPayment)
        {
            var validationError = Validate<CardPaymentModel, JudoApiErrorModel>(CardPaymentValidator, cardPayment);

            return validationError ?? PostInternal<CardPaymentModel, JudoApiErrorModel>(_validateAddress, cardPayment);
        }

        public Task<IResult<JudoApiErrorModel>> Validate(TokenPaymentModel tokenPayment)
        {
            var validationError = Validate<TokenPaymentModel, JudoApiErrorModel>(TokenPaymentValidator, tokenPayment);

            return validationError ?? PostInternal<TokenPaymentModel, JudoApiErrorModel>(_validateAddress, tokenPayment);
        }
    }
}
