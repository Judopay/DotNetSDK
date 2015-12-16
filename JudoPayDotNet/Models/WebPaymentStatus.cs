namespace JudoPayDotNet.Models
{
    /// <summary>
    /// What state is the webpayment Request in
    /// </summary>
    // ReSharper disable UnusedMember.Global
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
        Success = 3,

        /// <summary>
        /// Web payment expired
        /// </summary>
        Expired = 5,

        /// <summary>
        /// The payment for this payment request was cancelled
        /// </summary>
        Cancelled = 6,
    }
    // ReSharper restore UnusedMember.Global
}