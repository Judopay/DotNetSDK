using System.Threading.Tasks;
using FluentValidation;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;
using JudoPayDotNet.Models.Validations;

namespace JudoPayDotNet.Clients
{
    internal class RegisterCards : JudoPayClient, IRegisterCards
    {
        private readonly IValidator<RegisterCardModel> _validator = new RegisterCardValidator();
        private readonly IValidator<RegisterEncryptedCardModel> _encryptedValidator = new RegisterEncryptedCardValidator();

        private const string REGISTER_CARD_ADDRESS = "transactions/registercard";

        public RegisterCards(ILog logger, IClient client, bool deDuplicate = false)
            : base(logger, client)
        {
            _deDuplicateTransactions = deDuplicate;
        }

        public Task<IResult<ITransactionResult>> Create(RegisterCardModel registerCard)
        {
            var validationError = Validate<RegisterCardModel, ITransactionResult>(_validator, registerCard);
            return validationError ?? PostInternal<RegisterCardModel, ITransactionResult>(REGISTER_CARD_ADDRESS, registerCard);
        }

        public Task<IResult<ITransactionResult>> Create(RegisterEncryptedCardModel registerEncryptedCard)
        {
            var validationError = Validate<RegisterEncryptedCardModel, ITransactionResult>(_encryptedValidator, registerEncryptedCard);
            return validationError ?? PostInternal<RegisterEncryptedCardModel, ITransactionResult>(REGISTER_CARD_ADDRESS, registerEncryptedCard);
        }
    }
}
