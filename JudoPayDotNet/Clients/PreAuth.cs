using System.Threading.Tasks;
using JudoPayDotNet.Errors;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    internal class PreAuths : BasePreAuth, IPreAuths
    {
        private const string Createpreauthaddress = "transactions/preauths";
        private const string Validatepreauthaddress = "transactions/preauths/validate";

        protected readonly string ValidatePreAuthAddress;

        public PreAuths(ILog logger, IClient client)
            : this(logger, client, Createpreauthaddress, Validatepreauthaddress)
        {
        }

        public PreAuths(ILog logger, IClient client, string createAddress, string validateAddress)
            : base(logger, client, createAddress)
        {
            ValidatePreAuthAddress = validateAddress;
        }

        public Task<IResult<JudoApiErrorModel>> Validate(CardPaymentModel cardPreAuth)
        {
            var validationError = Validate<CardPaymentModel, JudoApiErrorModel>(CardPaymentValidator, cardPreAuth);

            return validationError ?? PostInternal<CardPaymentModel, JudoApiErrorModel>(ValidatePreAuthAddress, cardPreAuth);
        }

        public Task<IResult<JudoApiErrorModel>> Validate(TokenPaymentModel tokenPreAuth)
        {
            var validationError = Validate<TokenPaymentModel, JudoApiErrorModel>(TokenPaymentValidator, tokenPreAuth);

            return validationError ?? PostInternal<TokenPaymentModel, JudoApiErrorModel>(ValidatePreAuthAddress, tokenPreAuth);
        }
    }
}
