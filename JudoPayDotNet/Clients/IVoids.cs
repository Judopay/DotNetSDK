using System.Threading.Tasks;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    /// <summary>
    /// The entity responsible for voiding transactions
    /// </summary>
    /// <remarks>
    /// This entity allows you to void previously authorised (PreAuth transaction)
    /// </remarks>
    public interface IVoids
    {
        /// <summary>
        /// Creates the specified void.
        /// </summary>
        /// <param name="voidTransaction">The void.</param>
        /// <returns>The receipt for the created void.</returns>
        Task<IResult<ITransactionResult>> Create(VoidModel voidTransaction);
    }
}
