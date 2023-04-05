namespace JudoPayDotNet.Models
{
	/// <summary>
	/// The digital wallet used when processing this transaction
	/// </summary>
	public enum WalletType
	{
		/// <summary>
		/// No digital wallet was used
		/// </summary>
		None = 0,

		/// <summary>
		/// This transaction was processed using Apple Pay (DPAN)
		/// </summary>
		ApplePay = 1,

		// GooglePay can be FPAN or DPAN
        GooglePay = 4,

        // ClickToPay can be FPAN or DPAN
        ClickToPay = 5
 }
}