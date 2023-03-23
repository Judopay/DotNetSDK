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
        /// Resume a 3DS2 authorization following a device details check.
        /// </summary>
        /// <param name="receiptId">The Judopay transaction identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns>PaymentReceiptModel or a PaymentRequiresThreeDSecureTwoModel with
        /// a ChallengeUrl is a user challenge is required</returns>
        Task<IResult<ITransactionResult>> Resume3DSecureTwo(string receiptId, ResumeThreeDSecureTwoModel model);

        /// <summary>
        /// Complete a 3DS2 authorization following a completed challenge.
        /// </summary>
        /// <param name="receiptId">The Judopay transaction identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns>PaymentReceiptModel</returns>
        Task<IResult<PaymentReceiptModel>> Complete3DSecureTwo(string receiptId, CompleteThreeDSecureTwoModel model);
    }
}
