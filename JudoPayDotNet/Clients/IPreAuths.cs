using System.Threading.Tasks;
using JudoPayDotNet.Errors;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    /// <summary>
    /// The entity reponsible for providing pre authorizations operations
    /// </summary>
    public interface IPreAuths
    {
        /// <summary>
        /// Creates the specified card pre authorization.
        /// </summary>
        /// <param name="cardPreAuth">The card pre authorization.</param>
        /// <returns>The receipt for the created card pre authorization</returns>
        Task<IResult<PaymentReceiptModel>> Create(CardPaymentModel cardPreAuth);

        /// <summary>
        /// Creates the specified token pre authorization.
        /// </summary>
        /// <param name="tokenPreAuth">The token pre authorization.</param>
        /// <returns>The receipt for the created token pre authorization</returns>
        Task<IResult<PaymentReceiptModel>> Create(TokenPaymentModel tokenPreAuth);

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
    }
}