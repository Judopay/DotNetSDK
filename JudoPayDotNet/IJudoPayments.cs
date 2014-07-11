using JudoPayDotNet.Clients;

namespace JudoPayDotNet
{
    /// <summary>
    /// Judo payments sdk client
    /// </summary>
    public interface IJudoPayments
    {
        /// <summary>
        /// The entity reponsible for providing payments operations
        /// </summary>
        IPayments Payments { get; set; }

        /// <summary>
        /// The entity reponsible for providing refunds operations
        /// </summary>
        IRefunds Refunds { get; set; }

        /// <summary>
        /// The entity reponsible for providing pre authorizations operations
        /// </summary>
        IPreAuths PreAuths { get; set; }

        /// <summary>
        /// The entity reponsible for providing transactions operations
        /// </summary>
        /// <value>
        /// The transactions.
        /// </value>
        ITransactions Transactions { get; set; }

        /// <summary>
        /// The entity reponsible for providing collections operations
        /// </summary>
        ICollections Collections { get; set; }
    }
}