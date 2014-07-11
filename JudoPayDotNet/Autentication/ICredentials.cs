namespace JudoPayDotNet.Autentication
{
    /// <summary>
    /// The credentials to access Judo Payments
    /// </summary>
    public interface ICredentials
    {
        /// <summary>
        /// Gets the token for basic authentication.
        /// </summary>
        /// <value>
        /// The token for basic authentication.
        /// </value>
        string Token { get; }

        /// <summary>
        /// Gets the secret for basic authentication.
        /// </summary>
        /// <value>
        /// The secret for basic authentication.
        /// </value>
        string Secret { get; }

        /// <summary>
        /// Gets the OAuth 2 access token.
        /// </summary>
        /// <value>
        /// The OAuth 2 access token.
        /// </value>
        string OAuthAccessToken { get; }
    }
}