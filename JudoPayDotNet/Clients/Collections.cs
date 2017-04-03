using System.Threading.Tasks;
using FluentValidation;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;
using JudoPayDotNet.Models.Validations;

namespace JudoPayDotNet.Clients
{
    internal class Collections : JudoPayClient, ICollections
    {
        private const string CREATE_ADDRESS = "transactions/collections";

        private readonly IValidator<CollectionModel> CollectionValidator = new CollectionsValidator();

        public Collections(ILog logger, IClient client)
            : base(logger, client)
        {
        }

        public Task<IResult<ITransactionResult>> Create(CollectionModel collection)
        {
            var validationError = Validate<CollectionModel, ITransactionResult>(CollectionValidator, collection);
            return validationError ?? PostInternal<CollectionModel, ITransactionResult>(CREATE_ADDRESS, collection);
        }
    }
}