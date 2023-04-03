using System.Threading.Tasks;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    internal class RegisterCards : JudoPayClient, IRegisterCards
    {
        private const string REGISTER_CARD_ADDRESS = "transactions/registercard";

        public RegisterCards(ILog logger, IClient client, bool deDuplicate = false)
            : base(logger, client)
        {
            _deDuplicateTransactions = deDuplicate;
        }

        public Task<IResult<ITransactionResult>> Create(RegisterCardModel registerCard)
        {
            return PostInternal<RegisterCardModel, ITransactionResult>(REGISTER_CARD_ADDRESS, registerCard);
        }
    }
}
