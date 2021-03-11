using System.Threading.Tasks;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    internal class Voids : JudoPayClient, IVoids
    {
        private const string CREATE_ADDRESS = "transactions/voids";

        public Voids(ILog logger, IClient client)
            : base(logger, client)
        {
        }

        public Task<IResult<ITransactionResult>> Create(VoidModel voidTransaction)
        {
            return PostInternal<VoidModel, ITransactionResult>(CREATE_ADDRESS, voidTransaction);
        }
    }
}