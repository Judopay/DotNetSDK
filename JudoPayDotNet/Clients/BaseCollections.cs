using System.Threading.Tasks;
using FluentValidation;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;
using JudoPayDotNet.Models.Validations;

namespace JudoPayDotNet.Clients
{
    internal class BaseCollections : JudoPayClient
    {
        protected readonly IValidator<CollectionModel> CollectionValidator = new CollectionsValidator();
        protected readonly string CreateAddress;


        public BaseCollections(ILog logger, IClient client, string createAddress) : base(logger, client)
        {
            CreateAddress = createAddress;
        }

        public Task<IResult<ITransactionResult>> Create(CollectionModel collection)
        {
            var validationError = Validate<CollectionModel, ITransactionResult>(CollectionValidator, collection);

            return validationError ?? PostInternal<CollectionModel, ITransactionResult>(CreateAddress, collection);
        }
    }
}
