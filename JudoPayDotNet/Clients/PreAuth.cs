using System.Threading.Tasks;
using FluentValidation;
using JudoPayDotNet.Errors;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;
using JudoPayDotNet.Models.Validations;

namespace JudoPayDotNet.Clients
{
    internal class PreAuths : BasePreAuth, IPreAuths
    {
        private const string CREATEPREAUTHADDRESS = "transactions/preauths";
        private const string VALIDATEPREAUTHADDRESS = "transactions/preauths/validate";

        protected readonly string ValidatePreAuthAddress;

        public PreAuths(ILog logger, IClient client)
            : this(logger, client, CREATEPREAUTHADDRESS, VALIDATEPREAUTHADDRESS)
        {
        }

        public PreAuths(ILog logger, IClient client, string createAddress, string validateAddress)
            : base(logger, client, createAddress)
        {
            ValidatePreAuthAddress = validateAddress;
        }

        public Task<IResult<JudoApiErrorModel>> Validate(CardPaymentModel cardPreAuth)
        {
            var validationError = Validate<CardPaymentModel, JudoApiErrorModel>(cardPaymentValidator, cardPreAuth);

            return validationError ?? PostInternal<CardPaymentModel, JudoApiErrorModel>(ValidatePreAuthAddress, cardPreAuth);
        }

        public Task<IResult<JudoApiErrorModel>> Validate(TokenPaymentModel tokenPreAuth)
        {
            var validationError = Validate<TokenPaymentModel, JudoApiErrorModel>(tokenPaymentValidator, tokenPreAuth);

            return validationError ?? PostInternal<TokenPaymentModel, JudoApiErrorModel>(ValidatePreAuthAddress, tokenPreAuth);
        }
    }
}
