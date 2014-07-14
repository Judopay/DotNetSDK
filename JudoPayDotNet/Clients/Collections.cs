using System.Threading.Tasks;
using JudoPayDotNet.Client;
using JudoPayDotNet.Errors;
using JudoPayDotNet.Http;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    internal class Collections : JudoPayClient, ICollections
    {
        private const string CREATEADDRESS = "transactions/collections";
        private const string VALIDATEADDRESS = "transactions/collections/validate";

        public Collections(IClient client)
            : base(client)
        {
        }

        public Task<IResult<PaymentReceiptModel>> Create(CollectionModel collection)
        {
            return PostInternal<CollectionModel, PaymentReceiptModel>(CREATEADDRESS, collection);
        }

        public Task<IResult<JudoApiErrorModel>> Validate(CollectionModel collection)
        {
            return PostInternal<CollectionModel, JudoApiErrorModel>(VALIDATEADDRESS, collection);
        }
    }
}