namespace JudoPayDotNet.Clients.WebPayments
{
    /// <summary>
    /// Provides judo payment operations for webpayments
    /// </summary>
    // ReSharper disable UnusedMemberInSuper.Global
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
        /// The entity responsible for providing retrieval transactions operations
        /// </summary>
        ITransactions Transactions { get; set; }
    }
    // ReSharper restore UnusedMemberInSuper.Global
}