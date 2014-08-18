using System.Threading.Tasks;
using JudoPayDotNet.Errors;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    internal class Collections : BaseCollections, ICollections
    {
        private const string CREATE_ADDRESS = "transactions/collections";
        private const string VALIDATE_ADDRESS = "transactions/collections/validate";

        private readonly string _validateAddress;

        public Collections(ILog logger, IClient client)
            : this(logger, client, CREATE_ADDRESS, VALIDATE_ADDRESS)
        {
        }

        private Collections(ILog logger, IClient client, string createAddress, string validateAddress)
            : base(logger, client, createAddress)
        {
            _validateAddress = validateAddress;
        }

        public Task<IResult<JudoApiErrorModel>> Validate(CollectionModel collection)
        {
            var validationError = Validate<CollectionModel, JudoApiErrorModel>(CollectionValidator, collection);

            return validationError ?? PostInternal<CollectionModel, JudoApiErrorModel>(_validateAddress, collection);
        }
    }
}