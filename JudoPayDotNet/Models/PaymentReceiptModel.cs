using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudoPayDotNet.Models
{
    public class PaymentReceiptModel : ITransactionResult
    {
        public string ReceiptId { get; set; }

        /// <summary>
        /// The receipt id of the original payment, if this is a refund or collection
        /// </summary>
        public long? OriginalReceiptId { get; set; }

        /// <summary>
        /// Payment, Refund, CreateInternal, or Collection
        /// </summary>
        public string Type { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public string Result { get; set; }

        public string Message { get; set; }

        public long JudoId { get; set; }

        public string MerchantName { get; set; }

        public string AppearsOnStatementAs { get; set; }

        /// <summary>
        /// Refunds and PreAuths will not have this value
        /// </summary>
        public decimal? OriginalAmount { get; set; }

        /// <summary>
        /// Refunds and PreAuths will not have this value
        /// </summary>
        public decimal Refunds { get; set; }

        public decimal NetAmount { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public decimal PartnerServiceFee { get; set; }

        public CardDetails CardDetails { get; set; }

        public Consumer Consumer { get; set; }

        public int? RiskScore { get; set; }

        public IDictionary<string, string> YourPaymentMetaData { get; set; }

        /// <summary>
        /// If the payment requested 3d secure, we need to include the result of that authentication process
        /// </summary>
        public ThreeDSecureReceiptModel ThreeDSecure { get; set; }
    }
}
