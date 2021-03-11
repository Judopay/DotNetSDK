using System.Threading.Tasks;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    internal class Refunds : JudoPayClient, IRefunds
    {
        private const string CREATE_REFUNDS_ADDRESS = "transactions/refunds";

        public Refunds(ILog logger, IClient client)
            : base(logger, client)
        {
        }
        
        public Task<IResult<ITransactionResult>> Create(RefundModel refund)
        {
            return PostInternal<RefundModel, ITransactionResult>(CREATE_REFUNDS_ADDRESS, refund);
        }
    }
}
