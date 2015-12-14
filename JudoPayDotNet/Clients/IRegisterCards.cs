using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JudoPayDotNet.Errors;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    /// <summary>
    /// The entity responsible for processing and validating registar card requests
    /// </summary>
    /// <remarks>
    /// This entity allows you to register cards to the consumer
    /// </remarks>
    public interface IRegisterCards
    {
        /// <summary>
        /// Registers a consumer card.
        /// </summary>
        /// <param name="registerCard">The card to register.</param>
        /// <returns>The result of the registration of the card</returns>
        Task<IResult<ITransactionResult>> Create(CardPaymentModel registerCard);
    }
}
