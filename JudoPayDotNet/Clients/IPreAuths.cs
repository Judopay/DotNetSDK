using System.Threading.Tasks;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    /// <summary>
    /// The entity reponsible for providing pre authorizations operations
    /// </summary>
    public interface IPreAuths
    {
        /// <summary>
        /// Creates the specified card pre authentication.
        /// </summary>
        /// <param name="cardPreAuth">The card pre authorization.</param>
        /// <returns>The receipt for the created card pre authorization</returns>
        Task<IResult<PaymentReceiptModel>> Create(CardPaymentModel cardPreAuth);

        /// <summary>
        /// Creates the specified token pre authentication.
        /// </summary>
        /// <param name="tokenPreAuth">The token pre authorization.</param>
        /// <returns>The receipt for the created token pre authorization</returns>
        Task<IResult<PaymentReceiptModel>> Create(TokenPaymentModel tokenPreAuth);
    }
}