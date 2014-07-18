using System.Threading.Tasks;
using JudoPayDotNet.Errors;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    internal class Collections : BaseCollections, ICollections
    {
        private const string CREATEADDRESS = "transactions/collections";
        private const string VALIDATEADDRESS = "transactions/collections/validate";

        protected readonly string ValidateAddress;

        public Collections(ILog logger, IClient client)
            : this(logger, client, CREATEADDRESS, VALIDATEADDRESS)
        {
        }

        protected Collections(ILog logger, IClient client, string createAddress, string validateAddress)
            : base(logger, client, createAddress)
        {
            ValidateAddress = validateAddress;
        }

        public Task<IResult<JudoApiErrorModel>> Validate(CollectionModel collection)
        {
            var validationError = Validate<CollectionModel, JudoApiErrorModel>(collectionValidator, collection);

            return validationError ?? PostInternal<CollectionModel, JudoApiErrorModel>(ValidateAddress, collection);
        }
    }
}