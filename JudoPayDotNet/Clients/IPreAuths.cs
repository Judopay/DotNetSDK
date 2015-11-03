using System.Threading.Tasks;
using JudoPayDotNet.Errors;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    /// <summary>
    /// Provides the ability to reserve funds on your consumers cards. Those funds can later 
    /// be captured using a collection transaction.
    /// </summary>
    // ReSharper disable UnusedMember.Global
    public interface IPreAuths
    {
        /// <summary>
        /// Creates the specified card pre authorization.
        /// </summary>
        /// <param name="cardPreAuth">The card pre authorization.</param>
        /// <returns>The receipt for the created card pre authorization</returns>
        Task<IResult<ITransactionResult>> Create(CardPaymentModel cardPreAuth);

        /// <summary>
        /// Creates the specified token pre authorization.
        /// </summary>
        /// <param name="tokenPreAuth">The token pre authorization.</param>
        /// <returns>The receipt for the created token pre authorization</returns>
        Task<IResult<ITransactionResult>> Create(TokenPaymentModel tokenPreAuth);


        /// <summary>
        /// Creates the specified apple Pay pre authorization.
        /// </summary>
        /// <param name="pkPreAuth">The apple payment.</param>
        /// <returns>The receipt for the created apple pre authorization</returns>
        Task<IResult<ITransactionResult>> Create(PKPaymentModel pkPreAuth);


        /// <summary>
        /// Validates the specified card pre authorization.
        /// </summary>
        /// <param name="cardPayment">The card payment.</param>
        /// <returns>The result of validation of card pre authorization</returns>
        Task<IResult<JudoApiErrorModel>> Validate(CardPaymentModel cardPayment);

        /// <summary>
        /// Validates the specified token pre authorization.
        /// </summary>
        /// <param name="tokenPayment">The token payment.</param>
        /// <returns>The result of validation of token pre authorization</returns>
        Task<IResult<JudoApiErrorModel>> Validate(TokenPaymentModel tokenPayment);

        /// <summary>
        /// Validates the specified apple pre authorization.
        /// </summary>
        /// <param name="pkPreAuth">The apple  pre authorization.</param>
        /// <returns>If the apple payment is valid</returns>
        Task<IResult<JudoApiErrorModel>> Validate(PKPaymentModel pkPreAuth);
    }
    // ReSharper restore UnusedMember.Global
}