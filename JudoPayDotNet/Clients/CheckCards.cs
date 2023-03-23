using System.Threading.Tasks;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    internal class CheckCards : JudoPayClient, ICheckCard
    {
        private const string CheckCardAddress = "transactions/checkcard";

        public CheckCards(ILog logger, IClient client, bool deDuplicate = false)
            : base(logger, client)
        {
            _deDuplicateTransactions = deDuplicate;
        }

        public Task<IResult<ITransactionResult>> Create(CheckCardModel checkCardModel)
        {
            return PostInternal<CheckCardModel, ITransactionResult>(CheckCardAddress, checkCardModel);
        }
    }
}
