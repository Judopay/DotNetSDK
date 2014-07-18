using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;
using JudoPayDotNet.Models.Validations;

namespace JudoPayDotNet.Clients
{
    internal class BasePreAuth : JudoPayClient
    {
        protected readonly IValidator<CardPaymentModel> cardPaymentValidator = new CardPaymentValidator();
        protected readonly IValidator<TokenPaymentModel> tokenPaymentValidator = new TokenPaymentValidator();
        protected readonly string CreatePreAuthAddress;

        public BasePreAuth(ILog logger, IClient client, string createPreAuthAddress) : base(logger, client)
        {
            CreatePreAuthAddress = createPreAuthAddress;
        }

        public Task<IResult<ITransactionResult>> Create(CardPaymentModel cardPreAuth)
        {
            var validationError = Validate<CardPaymentModel, ITransactionResult>(cardPaymentValidator, cardPreAuth);

            return validationError ?? PostInternal<CardPaymentModel, ITransactionResult>(CreatePreAuthAddress, cardPreAuth);
        }

        public Task<IResult<ITransactionResult>> Create(TokenPaymentModel tokenPreAuth)
        {
            var validationError = Validate<TokenPaymentModel, ITransactionResult>(tokenPaymentValidator, tokenPreAuth);

            return validationError ?? PostInternal<TokenPaymentModel, ITransactionResult>(CreatePreAuthAddress, tokenPreAuth);
        }
    }
}
