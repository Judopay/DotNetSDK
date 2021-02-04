using System;
using System.Runtime.CompilerServices;

namespace JudoPayDotNet.Authentication
{
    /// <summary>
    /// A concrete implementation of our <see cref="ICredentials"/> interface. These credentials are used to access our 
    /// JudoPay API and can be either an API Token and Secret pair, an API Token with a Payment Session, or an OAuth 2.0 Access Token
    /// </summary>
    public class Credentials : ICredentials
    {
        public string Token { get; private set; }
        public string Secret { get; private set; }
        public string PaymentSession { get; private set; }

        public string OAuthAccessToken { get; private set; }

        public Credentials(string token, string secret)
        {
            Token = token;
            Secret = secret;
        }

        public Credentials(string oAuthAccessToken)
        {
            OAuthAccessToken = oAuthAccessToken;
        }

        public Credentials SetPaymentSession(string paymentSession)
        {
            PaymentSession = paymentSession;
            return this;
        }
    }
}
