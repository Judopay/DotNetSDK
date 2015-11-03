using System.Threading.Tasks;
using JudoPayDotNet.Errors;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    /// <summary>
    /// Provides immediate payment processing using either full card details, or a previously used card token
    /// </summary>
    /// <remarks>The amount specified by a payment is immediately captured</remarks>
    public interface IPayments
    {
        /// <summary>
        /// Creates the specified card payment.
        /// </summary>
        /// <param name="cardPayment">The card payment.</param>
        /// <returns>The receipt for the created card payment</returns>
        Task<IResult<ITransactionResult>> Create(CardPaymentModel cardPayment);

        /// <summary>
        /// Creates the specified token payment.
        /// </summary>
        /// <param name="tokenPayment">The token payment.</param>
        /// <returns>The receipt for the created token payment</returns>
        Task<IResult<ITransactionResult>> Create(TokenPaymentModel tokenPayment);


        /// <summary>
        /// Creates the specified apple payment.
        /// </summary>
        /// <param name="pkPayment">The apple payment.</param>
        /// <returns>The receipt for the created apple payment</returns>
        Task<IResult<ITransactionResult>> Create(PKPaymentModel pkPayment);

        /// <summary>
        /// Validates the specified card payment.
        /// </summary>
        /// <param name="cardPayment">The card payment.</param>
        /// <returns>If the card payment is valid</returns>
        Task<IResult<JudoApiErrorModel>> Validate(CardPaymentModel cardPayment);

        /// <summary>
        /// Validates the specified token payment.
        /// </summary>
        /// <param name="tokenPayment">The token payment.</param>
        /// <returns>If the token payment is valid</returns>
        Task<IResult<JudoApiErrorModel>> Validate(TokenPaymentModel tokenPayment);

        /// <summary>
        /// Validates the specified apple payment.
        /// </summary>
        /// <param name="pkPayment">The apple payment.</param>
        /// <returns>If the apple payment is valid</returns>
        Task<IResult<JudoApiErrorModel>> Validate(PKPaymentModel pkPayment);
    }
}