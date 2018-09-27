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
    }
}
