using System.Threading.Tasks;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    /// <summary>
    /// The entity responsible for processing and validating cards
    /// </summary>
    /// <remarks>
    /// This entity allows you to check the card of the consumer
    /// </remarks>
    public interface ICheckCard
    {
        /// <summary>
        /// Performs a card check against the card. This doesnt do an authorisation, it just checks whether or not the card is valid
        /// </summary>
        /// <param name="checkCardModel">The card to check.</param>
        /// <returns>The result of the card check</returns>
        Task<IResult<ITransactionResult>> Create(CheckCardModel checkCardModel);

        /// <summary>
        /// Performs a card check against the card using a One Use Token
        /// </summary>
        /// <param name="checkEncryptedCardModel">The card to check.</param>
        /// <returns>The result of the card check</returns>
        Task<IResult<ITransactionResult>> Create(CheckEncryptedCardModel checkEncryptedCardModel);
    }
}
