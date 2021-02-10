using System.Threading.Tasks;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    /// <summary>
    /// The entity responsible for providing 3D authorization operations
    /// </summary>
    public interface IThreeDs
    {
        /// <summary>
        /// Complete a 3D authorization.
        /// </summary>
		/// <param name="receiptId">The transaction identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns>Payment Receipt Model</returns>
        Task<IResult<PaymentReceiptModel>> Complete3DSecure(long receiptId, ThreeDResultModel model);

        /// <summary>
        /// Resume a 3DS2 authorization. 
        /// </summary>
        /// <param name="receiptId">The transaction identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns>Payment Receipt Model</returns>
        Task<IResult<ITransactionResult>> Resume3DSecureTwo(long receiptId, ResumeThreeDSecureTwoModel model);

        /// <summary>
        /// Complete a 3DS2 authorization.
        /// </summary>
        /// <param name="receiptId">The transaction identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns>Payment Receipt Model</returns>
        Task<IResult<PaymentReceiptModel>> Complete3DSecureTwo(long receiptId, CompleteThreeDSecureTwoModel model);
    }
}
