using System.Threading.Tasks;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    /// <summary>
    /// The entity responsible for processing and validating collection transactions
    /// </summary>
    /// <remarks>
    /// This entity allows you to collect previously authorised (PreAuth transaction)
    /// </remarks>
    public interface ICollections
    {
        /// <summary>
        /// Creates the specified collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <returns>The receipt for the created collection.</returns>
        Task<IResult<ITransactionResult>> Create(CollectionModel collection);
    }
}
