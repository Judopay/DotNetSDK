using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JudoPayDotNet.Http
{
    /// <summary>
    /// This insterface is intended to provided the ability of use of a custom client
    /// </summary>
    public interface IHttpClient
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}
