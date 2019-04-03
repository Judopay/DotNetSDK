using System.Threading.Tasks;
using FluentValidation;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;
using JudoPayDotNet.Models.Validations;

namespace JudoPayDotNet.Clients
{
    internal class CheckCards : JudoPayClient, ICheckCard
    {
        private readonly IValidator<CheckCardModel> _validator = new CheckCardValidator();
        private readonly IValidator<CheckEncryptedCardModel> _encryptedValidator = new CheckEncryptedCardValidator();

        private const string CheckCardAddress = "transactions/checkcard";

        public CheckCards(ILog logger, IClient client, bool deDuplicate = false)
            : base(logger, client)
        {
            _deDuplicateTransactions = deDuplicate;
        }

        public Task<IResult<ITransactionResult>> Create(CheckCardModel checkCardModel)
        {
            var validationError = Validate<CheckCardModel, ITransactionResult>(_validator, checkCardModel);
            return validationError ?? PostInternal<CheckCardModel, ITransactionResult>(CheckCardAddress, checkCardModel);
        }

        public Task<IResult<ITransactionResult>> Create(CheckEncryptedCardModel checkEncryptedCardModel)
        {
            var validationError = Validate<CheckEncryptedCardModel, ITransactionResult>(_encryptedValidator, checkEncryptedCardModel);
            return validationError ?? PostInternal<CheckEncryptedCardModel, ITransactionResult>(CheckCardAddress, checkEncryptedCardModel);
        }
    }
}
