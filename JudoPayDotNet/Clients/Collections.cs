using System.Threading.Tasks;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    internal class Collections : JudoPayClient, ICollections
    {
        private const string CREATE_ADDRESS = "transactions/collections";

        public Collections(ILog logger, IClient client)
            : base(logger, client)
        {
        }

        public Task<IResult<ITransactionResult>> Create(CollectionModel collection)
        {
            return PostInternal<CollectionModel, ITransactionResult>(CREATE_ADDRESS, collection);
        }
    }
}