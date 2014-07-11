namespace JudoPayDotNet.Models
{
    /// <summary>
    /// What state is the webpayment Request in
    /// </summary>
    public enum WebPaymentStatus : long
    {
        Unknown = 0,
        /// <summary>
        /// Payment is still pending
        /// </summary>
        Open = 1,

        /// <summary>
        /// This web payment has been completed
        /// </summary>
        Paid = 3,

        /// <summary>
        /// The payment for this payment request was cancelled
        /// </summary>
        Cancelled = 6
    }
}