using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JudoPayDotNet.Http
{
    /// <summary>
    /// This client is a simple wrapper of IHttpClient
    /// </summary>
    public class HttpClientWrapper : IHttpClient
    {
        private readonly HttpClient _httpClient;

        public HttpClientWrapper()
        {
            _httpClient = new HttpClient();
        }

        public HttpClientWrapper(HttpMessageHandler handler)
        {
            _httpClient = new HttpClient(handler);
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return _httpClient.SendAsync(request);
        }
    }
}
