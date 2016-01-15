using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JudoPayDotNet.Errors;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    internal class RegisterCards : BaseRegisterCards, IRegisterCards
    {

        private const string CREATE_ADDRESS = "transactions/registercard";
        private const string VALIDATE_ADDRESS = "transactions/registercard/validate";

        private readonly string _validateAddress;

        public RegisterCards(ILog logger, IClient client,bool deDuplicate=false )
            : this(logger, client, CREATE_ADDRESS, VALIDATE_ADDRESS)
        {
            DeDuplicateTransactions = deDuplicate;
        }

        private RegisterCards(ILog logger, IClient client, string createAddress, string validateAddress)
            : base(logger, client, createAddress)
        {
            _validateAddress = validateAddress;
        }
    }
}
