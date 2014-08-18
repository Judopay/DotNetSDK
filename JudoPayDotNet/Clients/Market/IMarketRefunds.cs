using System.Threading.Tasks;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients.Market
{
    /// <summary>
    /// The entity responsible for listing and processing refunds for your marketplace sellers.
    /// </summary>
    public interface IMarketRefunds
    {
        /// <summary>
        /// Creates the specified refund.
        /// </summary>
        /// <param name="refund">The refund.</param>
        /// <returns>The receipt for the created refund</returns>
        Task<IResult<ITransactionResult>> Create(RefundModel refund);
    }
}