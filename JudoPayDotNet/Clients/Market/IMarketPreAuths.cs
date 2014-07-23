using System.Threading.Tasks;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients.Market
{
    /// <summary>
    /// The entity responsible for providing market pre authorizations operations
    /// </summary>
    // ReSharper disable UnusedMember.Global
    public interface IMarketPreAuths
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
    }
    // ReSharper restore UnusedMember.Global
}