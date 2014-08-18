using System.Threading.Tasks;
using JudoPayDotNet.Errors;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    /// <summary>
    /// The entity responsible for providing payments operations
    /// </summary>
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
    }
}