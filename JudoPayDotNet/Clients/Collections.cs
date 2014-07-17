using System;
using System.Threading.Tasks;
using FluentValidation;
using JudoPayDotNet.Errors;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;
using JudoPayDotNet.Models.Validations;

namespace JudoPayDotNet.Clients
{
    internal class Collections : JudoPayClient, ICollections
    {
        private const string CREATEADDRESS = "transactions/collections";
        private const string VALIDATEADDRESS = "transactions/collections/validate";
        private readonly IValidator<CollectionModel> collectionValidator = new CollectionsValidator();

        public Collections(ILog logger, IClient client)
            : base(logger, client)
        {
        }

        public Task<IResult<ITransactionResult>> Create(CollectionModel collection)
        {
            var validationError = Validate<CollectionModel, ITransactionResult>(collectionValidator, collection);

            return validationError ?? PostInternal<CollectionModel, ITransactionResult>(CREATEADDRESS, collection);
        }

        public Task<IResult<JudoApiErrorModel>> Validate(CollectionModel collection)
        {
            var validationError = Validate<CollectionModel, JudoApiErrorModel>(collectionValidator, collection);

            return validationError ?? PostInternal<CollectionModel, JudoApiErrorModel>(VALIDATEADDRESS, collection);
        }
    }
}