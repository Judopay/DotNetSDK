namespace JudoPayDotNet.Clients.WebPayments
{
	/// <summary>
	/// This entity is our webpayments (judoResponsive) endpoint. Webpayments are used in web applications when you wish to 
	/// pass your user to judo when processing their payment.
	/// </summary>
	/// <remarks>Webpayments are used when you do not want to handle the payment yourself.</remarks>
	internal class WebPayments : IWebPayments
    {
		/// <summary>
		/// Allows you to create a (payment) webpayment before passing Judo your customer to complete the payment
		/// </summary>
		public IPayments Payments { get; set; }

		/// <summary>
		/// Allows you to create a (preauth) webpayment before passing Judo your customer to complete the payment
		/// </summary>
		public IPreAuths PreAuths { get; set; }

		/// <summary>
		/// Allows you to fetch details of an individual webpayment (either by receipt id or reference)
		/// </summary>
		public ITransactions Transactions { get; set; }
    }
}
