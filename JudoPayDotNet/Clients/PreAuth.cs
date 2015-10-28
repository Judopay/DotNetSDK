using System.Threading.Tasks;
using JudoPayDotNet.Errors;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    internal class PreAuths : BasePreAuth, IPreAuths
    {
        private const string CREATE_PREAUTH_ADDRESS = "transactions/preauths";
        private const string VALIDATE_PREAUTH_ADDRESS = "transactions/preauths/validate";

        private readonly string _validatePreAuthAddress;

        public PreAuths(ILog logger, IClient client, 
                            string createAddress = CREATE_PREAUTH_ADDRESS, 
                            string validateAddress = VALIDATE_PREAUTH_ADDRESS)
            : base(logger, client, createAddress)
        {
            _validatePreAuthAddress = validateAddress;
        }

        public Task<IResult<JudoApiErrorModel>> Validate(CardPaymentModel cardPreAuth)
        {
            var validationError = Validate<CardPaymentModel, JudoApiErrorModel>(CardPaymentValidator, cardPreAuth);

            return validationError ?? PostInternal<CardPaymentModel, JudoApiErrorModel>(_validatePreAuthAddress, cardPreAuth);
        }

        public Task<IResult<JudoApiErrorModel>> Validate(TokenPaymentModel tokenPreAuth)
        {
            var validationError = Validate<TokenPaymentModel, JudoApiErrorModel>(TokenPaymentValidator, tokenPreAuth);

            return validationError ?? PostInternal<TokenPaymentModel, JudoApiErrorModel>(_validatePreAuthAddress, tokenPreAuth);
        }

        public Task<IResult<JudoApiErrorModel>> Validate(PKPaymentModel pkPreAuth)
        {
            var validationError = Validate<PKPaymentModel, JudoApiErrorModel>(PKPaymentValidator, pkPreAuth);

            return validationError ?? PostInternal<PKPaymentModel, JudoApiErrorModel>(_validatePreAuthAddress, pkPreAuth);
        }
    }
}
