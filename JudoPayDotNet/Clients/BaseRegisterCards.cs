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
        protected readonly IValidator<CardPaymentModel> RegisterCardValidator = new CardPaymentValidator();
        private readonly string _createAddress;


        protected BaseRegisterCards(ILog logger, IClient client, string createAddress)
            : base(logger, client)
        {
            _createAddress = createAddress;
        }

        public Task<IResult<ITransactionResult>> Create(CardPaymentModel registerCard)
        {
            var validationError = Validate<CardPaymentModel, ITransactionResult>(RegisterCardValidator, registerCard);
            registerCard.ProvisionSDKVersion();
            return validationError ?? PostInternal<CardPaymentModel, ITransactionResult>(_createAddress, registerCard);
        }
    }
}
