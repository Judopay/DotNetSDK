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
        private readonly IValidator<CardPaymentModel> SaveCardValidator = new CardPaymentValidator();

        private const string SAVE_CARD_ADDRESS = "transactions/savecard";

        public SaveCard(ILog logger, IClient client, bool deDuplicate = false)
            : base(logger, client)
        {
            _deDuplicateTransactions = deDuplicate;
        }

        public Task<IResult<ITransactionResult>> Create(CardPaymentModel saveCard)
        {
            var validationError = Validate<CardPaymentModel, ITransactionResult>(SaveCardValidator, saveCard);
            return validationError ?? PostInternal<CardPaymentModel, ITransactionResult>(SAVE_CARD_ADDRESS, saveCard);
        }
    }
}
