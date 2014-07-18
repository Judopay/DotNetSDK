using System.Threading.Tasks;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients.Market
{
    /// <summary>
    /// The entity reponsible for providing market merchant operations
    /// </summary>
    public interface IMarketMerchants
    {
        /// <summary>
        /// Gets the specified merchants by locator identifier.
        /// </summary> 
        /// <param name="locatorId">The locator identifier.</param>
        /// <returns></returns>
        Task<IResult<MerchantSearchResults>> Get(string locatorId);

        /// <summary>
        /// Gets merchants
        /// </summary>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="sort">The sort.</param>
        /// <returns></returns>
        Task<IResult<MerchantSearchResults>> Get(long? pageSize = null,
            long? offset = null,
            string sort = null);
    }
}