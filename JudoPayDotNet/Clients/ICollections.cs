using System.Threading.Tasks;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    /// <summary>
    /// The entity reponsible for providing collections operations
    /// </summary>
    public interface ICollections
    {
        /// <summary>
        /// Creates the specified collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <returns>The receipt for the created collection</returns>
        Task<IResult<PaymentReceiptModel>> Create(CollectionModel collection);
    }
}
