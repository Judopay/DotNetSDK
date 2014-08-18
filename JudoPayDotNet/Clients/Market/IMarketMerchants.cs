using System.Threading.Tasks;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients.Market
{
    /// <summary>
    /// The entity responsible for listing sellers (merchants) in your marketplace
    /// </summary>
    // ReSharper disable UnusedMember.Global
    public interface IMarketMerchants
    {
        /// <summary>
        /// Gets the specified merchants by locator identifier. The reference for an account and it's application
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
            TransactionListSorts? sort = null);
    }
    // ReSharper restore UnusedMember.Global
}