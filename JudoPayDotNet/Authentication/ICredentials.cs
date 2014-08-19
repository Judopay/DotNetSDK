namespace JudoPayDotNet.Authentication
{
    // ReSharper disable UnusedMemberInSuper.Global
	/// <summary>
	/// Your credentials to access our JudoPay API these can be either an API Token and 
	/// Secret pair, or an OAuth 2.0 Access Token
	/// </summary>
    public interface ICredentials
    {
        /// <summary>
        /// Gets the API token for basic authentication.
        /// </summary>
        /// <value>
        /// The token for basic authentication.
        /// </value>
        string Token { get; }

        /// <summary>
        /// Gets the API secret for basic authentication.
        /// </summary>
        /// <value>
        /// The secret for basic authentication.
        /// </value>
        string Secret { get; }

        /// <summary>
        /// Returns your seller's OAuth 2 access token.
        /// </summary>
        /// <value>
        /// The OAuth 2 access token.
        /// </value>
        string OAuthAccessToken { get; }
    }
    // ReSharper restore UnusedMemberInSuper.Global
}