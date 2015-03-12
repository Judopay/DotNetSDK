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
    internal class BaseRegisterCards : JudoPayClient
    {
        protected readonly IValidator<RegisterCardModel> RegisterCardValidator = new RegisterCardValidator();
        private readonly string _createAddress;


        protected BaseRegisterCards(ILog logger, IClient client, string createAddress)
            : base(logger, client)
        {
            _createAddress = createAddress;
        }

        public Task<IResult<ITransactionResult>> Create(RegisterCardModel registerCard)
        {
            var validationError = Validate<RegisterCardModel, ITransactionResult>(RegisterCardValidator, registerCard);

            return validationError ?? PostInternal<RegisterCardModel, ITransactionResult>(_createAddress, registerCard);
        }
    }
}
