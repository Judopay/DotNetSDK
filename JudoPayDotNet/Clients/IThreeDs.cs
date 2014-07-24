using System.Threading.Tasks;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    /// <summary>
    /// The entity responsibile for providing 3D authorization operations
    /// </summary>
    public interface IThreeDs
    {
        /// <summary>
        /// Get 3D Authorization data.
        /// </summary>
        /// <param name="md">The md.</param>
        /// <returns>3D Authorization data.</returns>
        Task<IResult<PaymentRequiresThreeDSecureModel>> GetThreeDAuthorization(string md);

        /// <summary>
        /// Complete a 3D authorization.
        /// </summary>
        /// <param name="receiptId">The receipt identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<IResult<PaymentReceiptModel>> Complete3DSecure(string receiptId, ThreeDResultModel model);
    }
}
