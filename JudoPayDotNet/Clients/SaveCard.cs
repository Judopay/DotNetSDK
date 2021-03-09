using System.Threading.Tasks;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    internal class SaveCard : JudoPayClient, ISaveCard
    {
        private const string SAVE_CARD_ADDRESS = "transactions/savecard";

        public SaveCard(ILog logger, IClient client, bool deDuplicate = false)
            : base(logger, client)
        {
            _deDuplicateTransactions = deDuplicate;
        }

        public Task<IResult<ITransactionResult>> Create(SaveCardModel saveCard)
        {
            return PostInternal<SaveCardModel, ITransactionResult>(SAVE_CARD_ADDRESS, saveCard);
        }

        public Task<IResult<ITransactionResult>> Create(SaveEncryptedCardModel saveEncryptedCard)
        {
            return PostInternal<SaveEncryptedCardModel, ITransactionResult>(SAVE_CARD_ADDRESS, saveEncryptedCard);
        }
    }
}
