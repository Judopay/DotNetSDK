using System.Threading.Tasks;
using JudoPayDotNet.Errors;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    internal class Collections : BaseCollections, ICollections
    {
        private const string Createaddress = "transactions/collections";
        private const string Validateaddress = "transactions/collections/validate";

        protected readonly string ValidateAddress;

        public Collections(ILog logger, IClient client)
            : this(logger, client, Createaddress, Validateaddress)
        {
        }

        protected Collections(ILog logger, IClient client, string createAddress, string validateAddress)
            : base(logger, client, createAddress)
        {
            ValidateAddress = validateAddress;
        }

        public Task<IResult<JudoApiErrorModel>> Validate(CollectionModel collection)
        {
            var validationError = Validate<CollectionModel, JudoApiErrorModel>(CollectionValidator, collection);

            return validationError ?? PostInternal<CollectionModel, JudoApiErrorModel>(ValidateAddress, collection);
        }
    }
}