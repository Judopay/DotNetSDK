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

        public AuthorizationHandler(Credentials credentials, ILog log)
        {
            _log = log;
            _credentials = credentials;

            _authenticationType = AuthType.Unknown;

            if (!String.IsNullOrWhiteSpace(_credentials.OAuthAccessToken))
            {
                _authenticationType = AuthType.Bearer;
            }
            else if (!String.IsNullOrWhiteSpace(_credentials.Token) && !String.IsNullOrWhiteSpace(_credentials.Secret))
            {
                _authenticationType = AuthType.Basic;
            }
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string schema = null;
            string parameter = null;

            switch (_authenticationType)
            {
                case AuthType.Basic:
                    var full = string.Format("{0}:{1}", _credentials.Token, _credentials.Secret);

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

            if (!String.IsNullOrWhiteSpace(schema) && !string.IsNullOrWhiteSpace(parameter)) 
            { 
                request.Headers.Authorization = new AuthenticationHeaderValue(schema, parameter);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
