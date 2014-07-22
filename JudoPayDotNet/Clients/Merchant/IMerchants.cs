using System.Threading.Tasks;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients.Merchant
{
    /// <summary>
    /// The entity responsible for providing merchant operations
    /// </summary>
    public interface IMerchants
    {
        /// <summary>
        /// Gets the merchant by judo identifier.
        /// </summary>
        /// <param name="judoId">The judo identifier.</param>
        /// <returns></returns>
        Task<IResult<MerchantModel>> Get(string judoId);
    }
}