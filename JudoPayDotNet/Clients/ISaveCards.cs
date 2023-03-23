using System.Threading.Tasks;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    /// <summary>
    /// Saving (tokenising) a card without any calls to the card issuer return a string card token
    /// that can be used instead of the card PAN and expiry date in future transactions.
    /// </summary>
    public interface ISaveCard
    {
        /// <summary>
        /// Saves a consumer card without any calls to the card issuer and returns a string card token
        /// that can be used instead of the card PAN and expiry date in future transactions.
        /// </summary>
        /// <param name="saveCard">The card details to save.</param>
        /// <returns>The result of the save card call</returns>
        Task<IResult<ITransactionResult>> Create(SaveCardModel saveCard);
    }
}
