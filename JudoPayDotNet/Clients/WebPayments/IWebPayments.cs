namespace JudoPayDotNet.Clients.WebPayments
{
    /// <summary>
    /// Provides operations for webpayments
    /// </summary>
    // ReSharper disable UnusedMemberInSuper.Global
    public interface IWebPayments
    {
		/// <summary>
		/// Allows you to create a (payment) webpayment before passing Judo your customer to complete the payment
		/// </summary>
        IPayments Payments { get; set; }

		/// <summary>
		/// Allows you to create a (preauth) webpayment before passing Judo your customer to complete the payment
		/// </summary>
        IPreAuths PreAuths { get; set; }

        /// <summary>
		/// Allows you to fetch details of an individual webpayment (either by receipt id or reference)
        /// </summary>
        ITransactions Transactions { get; set; }
    }
    // ReSharper restore UnusedMemberInSuper.Global
}