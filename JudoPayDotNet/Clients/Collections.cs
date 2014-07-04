using System.Threading.Tasks;
using JudoPayDotNet.Client;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    internal class Collections : JudoPayClient, ICollections
    {
        private const string ADDRESS = "";

        public Collections(IClient client)
            : base(client, ADDRESS)
        {
        }

        public Task<IResult<PaymentReceiptModel>> Create(CollectionModel collection)
        {
            return CreateInternal<CollectionModel, PaymentReceiptModel>(collection);
        }
    }
}