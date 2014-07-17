using System.Threading.Tasks;
using FluentValidation;
using JudoPayDotNet.Errors;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;
using JudoPayDotNet.Models.Validations;

namespace JudoPayDotNet.Clients
{
    internal class PreAuths : JudoPayClient, IPreAuths
    {
        private const string CREATEPREAUTHADDRESS = "transactions/preauths";
        private const string VALIDATEPREAUTHADDRESS = "transactions/preauths/validate";
        private readonly IValidator<CardPaymentModel> cardPaymentValidator = new CardPaymentValidator();
        private readonly IValidator<TokenPaymentModel> tokenPaymentValidator = new TokenPaymentValidator();

        public PreAuths(ILog logger, IClient client)
            : base(logger, client)
        {
        }

        public Task<IResult<ITransactionResult>> Create(CardPaymentModel cardPreAuth)
        {
            var validationError = Validate<CardPaymentModel, ITransactionResult>(cardPaymentValidator, cardPreAuth);

            return validationError ?? PostInternal<CardPaymentModel, ITransactionResult>(CREATEPREAUTHADDRESS, cardPreAuth);
        }

        public Task<IResult<ITransactionResult>> Create(TokenPaymentModel tokenPreAuth)
        {
            var validationError = Validate<TokenPaymentModel, ITransactionResult>(tokenPaymentValidator, tokenPreAuth);

            return validationError ?? PostInternal<TokenPaymentModel, ITransactionResult>(CREATEPREAUTHADDRESS, tokenPreAuth);
        }

        public Task<IResult<JudoApiErrorModel>> Validate(CardPaymentModel cardPreAuth)
        {
            var validationError = Validate<CardPaymentModel, JudoApiErrorModel>(cardPaymentValidator, cardPreAuth);

            return validationError ?? PostInternal<CardPaymentModel, JudoApiErrorModel>(VALIDATEPREAUTHADDRESS, cardPreAuth);
        }

        public Task<IResult<JudoApiErrorModel>> Validate(TokenPaymentModel tokenPreAuth)
        {
            var validationError = Validate<TokenPaymentModel, JudoApiErrorModel>(tokenPaymentValidator, tokenPreAuth);

            return validationError ?? PostInternal<TokenPaymentModel, JudoApiErrorModel>(VALIDATEPREAUTHADDRESS, tokenPreAuth);
        }
    }
}
