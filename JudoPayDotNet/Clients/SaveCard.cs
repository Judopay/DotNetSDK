using System.Threading.Tasks;
using FluentValidation;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;
using JudoPayDotNet.Models.Validations;

namespace JudoPayDotNet.Clients
{
    internal class SaveCard : JudoPayClient, ISaveCard
    {
        private readonly IValidator<SaveCardModel> _validator = new SaveCardValidator();
        private readonly IValidator<SaveEncryptedCardModel> _encryptedValidator = new SaveEncryptedCardValidator();


        private const string SAVE_CARD_ADDRESS = "transactions/savecard";

        public SaveCard(ILog logger, IClient client, bool deDuplicate = false)
            : base(logger, client)
        {
            _deDuplicateTransactions = deDuplicate;
        }

        public Task<IResult<ITransactionResult>> Create(SaveCardModel saveCard)
        {
            var validationError = Validate<SaveCardModel, ITransactionResult>(_validator, saveCard);
            return validationError ?? PostInternal<SaveCardModel, ITransactionResult>(SAVE_CARD_ADDRESS, saveCard);
        }

        public Task<IResult<ITransactionResult>> Create(SaveEncryptedCardModel saveEncryptedCard)
        {
            var validationError = Validate<SaveEncryptedCardModel, ITransactionResult>(_encryptedValidator, saveEncryptedCard);
            return validationError ?? PostInternal<SaveEncryptedCardModel, ITransactionResult>(SAVE_CARD_ADDRESS, saveEncryptedCard);
        }
    }
}
