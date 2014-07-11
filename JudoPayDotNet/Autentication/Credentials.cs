namespace JudoPayDotNet.Autentication
{
    public class Credentials : ICredentials
    {
        public string Token { get; set; }
        public string Secret { get; set; }

        public string OAuthAccessToken { get; set; }

        public Credentials(string token, string secret)
        {
            Token = token;
            Secret = secret;
        }

        public Credentials(string oauthAccessToken)
        {
            OAuthAccessToken = oauthAccessToken;
        }
    }
}
