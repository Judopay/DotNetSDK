using System.Threading.Tasks;
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
        /// Creates the specified Apple Pay payment.
        /// </summary>
        /// <param name="pkPayment">The Apple Pay payment.</param>
        /// <returns>The receipt for the created Apple Pay payment</returns>
        Task<IResult<ITransactionResult>> Create(PKPaymentModel pkPayment);

        /// <summary>
        /// Creates the specified Android Pay payment.
        /// </summary>
        /// <param name="androidPayment">The Android Pay payment.</param>
        /// <returns>The receipt for the created Android Pay payment</returns>
        Task<IResult<ITransactionResult>> Create(AndroidPaymentModel androidPayment);

        /// <summary>
        /// Creates the specified One time payment.
        /// </summary>
        /// <param name="oneTimePayment">The one time payment payload.</param>
        /// <returns>The receipt for the created payment</returns>
        Task<IResult<ITransactionResult>> Create(OneTimePaymentModel oneTimePayment);
    }
}