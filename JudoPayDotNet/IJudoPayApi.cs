using JudoPayDotNet.Clients;
using JudoPayDotNet.Clients.WebPayments;
using JudoPayDotNet.Http;
using IPayments = JudoPayDotNet.Clients.IPayments;
using IPreAuths = JudoPayDotNet.Clients.IPreAuths;
using ITransactions = JudoPayDotNet.Clients.ITransactions;

namespace JudoPayDotNet
{
	/// <summary>
	/// The JudoPay API client, the main entry point for the SDK
	/// </summary>
	/// <remarks>This interface is provided to make it easy to mock the judopay api client</remarks>
    // ReSharper disable UnusedMemberInSuper.Global
    public interface IJudoPayApi
    {
        /// <summary>
        /// Details about the connection used to interact with the judopay api
        /// </summary>
        Connection Connection { get; }

        /// <summary>
		/// Provides operations for webpayments
		/// </summary>
        IWebPayments WebPayments { get; set; }

        /// <summary>
		/// Provides immediate payment processing using either full card details, or a previously used card token
		/// </summary>
        IPayments Payments { get; set; }

		/// <summary>
		/// Refund previous transactions
		/// </summary>
        IRefunds Refunds { get; set; }

        /// <summary>
        /// Process preauth transactions, reserving funds on a consumer's card but not collecting them.
        /// </summary>
        IPreAuths PreAuths { get; set; }

		/// <summary>
		/// Provides a combined view of all transactions within your account, and allows you to retrieve individual 
		/// transactions by their receipt id
		/// </summary>
        ITransactions Transactions { get; set; }

        /// <summary>
        /// Allows you to collect previously authorised transactions (<see cref="PreAuths"/>).
        /// </summary>
        ICollections Collections { get; set; }

        /// <summary>
        /// Used to retrieve information about inprogress 3D authorization requests
        /// </summary>
        IThreeDs ThreeDs { get; set; }

        /// <summary>
        /// Enables the developer to register a consumer card
        /// </summary>
        IRegisterCards RegisterCards { get; set; }

        /// <summary>
        /// Allows you voids previously authorised transactions (<see cref="PreAuths"/>).
        /// </summary>
        IVoids Voids { get; set; }
    }
    // ReSharper restore UnusedMemberInSuper.Global
}