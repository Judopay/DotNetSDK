using System.Threading.Tasks;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients.Market
{
    /// <summary>
    /// The entity reponsible for providing market collections operations
    /// </summary>
    public interface IMarketCollections
    {
        /// <summary>
        /// Creates the specified collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <returns>The receipt for the created collection.</returns>
        Task<IResult<ITransactionResult>> Create(CollectionModel collection);
    }
}