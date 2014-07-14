using System.Threading.Tasks;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    /// <summary>
    /// The entity responsibile for providing ThreeD authorization operations
    /// </summary>
    public interface IThreeDs
    {
        /// <summary>
        /// Get ThreeD Authorization data.
        /// </summary>
        /// <param name="md">The md.</param>
        /// <returns>ThreeD Authorization data.</returns>
        Task<IResult<PaymentRequiresThreeDSecureModel>> GetThreeDAuthorization(string md);

        /// <summary>
        /// Complete a ThreeD authorization.
        /// </summary>
        /// <param name="receiptId">The receipt identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<IResult<PaymentReceiptModel>> Complete3DSecure(string receiptId, ThreeDResultModel model);
    }
}
