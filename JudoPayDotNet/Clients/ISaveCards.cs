using System.Threading.Tasks;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    /// <summary>
    /// Saving (tokenising) a card without any calls to the card issuer 
    /// </summary>
    /// <remarks>
    /// This entity allows you to save cards to the consumer
    /// </remarks>
    public interface ISaveCard
    {
        /// <summary>
        /// Saves a consumer card.
        /// </summary>
        /// <param name="saveCard">The card to save (tokenise).</param>
        /// <returns>The result of the save card call</returns>
        Task<IResult<ITransactionResult>> Create(CardPaymentModel saveCard);
    }
}
