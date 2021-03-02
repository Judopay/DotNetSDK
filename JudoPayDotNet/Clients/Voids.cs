using System.Threading.Tasks;
using FluentValidation;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;
using JudoPayDotNet.Models.Validations;

namespace JudoPayDotNet.Clients
{
    internal class Voids : JudoPayClient, IVoids
    {
        private const string CREATE_ADDRESS = "transactions/voids";

        private readonly IValidator<VoidModel> voidValidator = new VoidsValidator();

        public Voids(ILog logger, IClient client)
            : base(logger, client)
        {
        }

        public Task<IResult<ITransactionResult>> Create(VoidModel voidTransaction)
        {
            var validationError = Validate<VoidModel, ITransactionResult>(voidValidator, voidTransaction);
            return validationError != null ? Task.FromResult(validationError) :
                PostInternal<VoidModel, ITransactionResult>(CREATE_ADDRESS, voidTransaction);
        }
    }
}