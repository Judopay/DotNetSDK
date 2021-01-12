using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JudoPayDotNet.Authentication;
using JudoPayDotNet.Logging;

namespace JudoPayDotNet.Http
{
	internal class AuthorizationHandler : DelegatingHandler
    {
        private readonly ILog _log;
        private readonly Credentials _credentials;
        private readonly AuthType _authenticationType;

        public const string PAYMENT_SESSION_HEADER = "Payment-Session";
        public const string API_TOKEN_HEADER = "Api-Token";

        public AuthorizationHandler(Credentials credentials, ILog log)
        {
            _log = log;
            _credentials = credentials;

            _authenticationType = AuthType.Unknown;

            if (!string.IsNullOrWhiteSpace(_credentials.OAuthAccessToken))
            {
                _authenticationType = AuthType.Bearer;
            }
            else if (!string.IsNullOrWhiteSpace(_credentials.Token) && 
                (!string.IsNullOrWhiteSpace(_credentials.Secret) || !string.IsNullOrWhiteSpace(_credentials.PaymentSession)))
            {
                _authenticationType = AuthType.Basic;
            }
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(_credentials.PaymentSession))
            {
                request.Headers.Add(API_TOKEN_HEADER, _credentials.Token);
                request.Headers.Add(PAYMENT_SESSION_HEADER, _credentials.PaymentSession);
            }
            else
            {
                string schema = null;
                string parameter = null;

                switch (_authenticationType)
                {
                    case AuthType.Basic:
                        var full = $"{_credentials.Token}:{_credentials.Secret}";

                        schema = AuthType.Basic.ToString();
                        var authDetails = Encoding.GetEncoding("iso-8859-1").GetBytes(full);
                        parameter = Convert.ToBase64String(authDetails);

                        break;

                    case AuthType.Bearer:
                        schema = AuthType.Bearer.ToString();
                        parameter = _credentials.OAuthAccessToken;

                        break;

                    default:
                        _log.Warn("Unknown Authorization scheme");

                        break;
                }

                if (!string.IsNullOrWhiteSpace(schema) && !string.IsNullOrWhiteSpace(parameter))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue(schema, parameter);
                }
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
