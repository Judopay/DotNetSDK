namespace JudoPayDotNet.Authentication
{
    public class Credentials : ICredentials
    {
        public string Token { get; private set; }
        public string Secret { get; private set; }

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
    }
}
