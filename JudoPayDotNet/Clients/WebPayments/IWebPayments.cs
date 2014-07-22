namespace JudoPayDotNet.Clients.WebPayments
{
    /// <summary>
    /// Provides judo payment operations for webpayments
    /// </summary>
    public interface IWebPayments
    {
        /// <summary>
        /// The entity responsible for providing payments operations
        /// </summary>
        IPayments Payments { get; set; }

        /// <summary>
        /// The entity responsible for providing preauths operations
        /// </summary>
        IPreAuths PreAuths { get; set; }

        /// <summary>
        /// The entity responsible for providing retrievel transactions operations
        /// </summary>
        ITransactions Transactions { get; set; }
    }
}