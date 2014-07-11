using System.Net.Http;
using System.Threading.Tasks;

namespace JudoPayDotNet.Http
{
    /// <summary>
    /// This interface is intended to provided the ability of use of a custom client
    /// </summary>
    public interface IHttpClient
    {
        /// <summary>
        /// Do an asynchronous Http.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The response.</returns>
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}
