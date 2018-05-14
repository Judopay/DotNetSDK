using System.Collections.Generic;
using System.Threading.Tasks;

namespace JudoPayDotNet.Http
{
    /// <summary>
    /// An HTTP client for Judo Pay api that provides http verb methods with the capibility 
    /// of assembling query string and body
    /// </summary>
    // ReSharper disable UnusedMember.Global
    public interface IClient
    {
        /// <summary>
        /// Do a http GET to the specified address.
        /// </summary>
        /// <typeparam name="T">The type of response model.</typeparam>
        /// <param name="address">The address.</param>
        /// <param name="parameters">The parameters for query string.</param>
        /// <param name="extraHeaders">Any extra http headers to send with the request</param>
        /// <returns>The response from Judy Pay with information of errors if they have ocurred</returns>
        Task<IResponse<T>> Get<T>(string address, Dictionary<string, string> parameters = null, 
            Dictionary<string, string> extraHeaders = null);

        /// <summary>
        /// Do a http POST to the specified address.
        /// </summary>
        /// <typeparam name="T">The type of response model.</typeparam>
        /// <param name="address">The address.</param>
        /// <param name="parameters">The parameters for query string.</param>
        /// <param name="extraHeaders">Any extra http headers to send with the request</param>
        /// <param name="body">The body.</param>
        /// <returns>The response from Judy Pay with information of errors if they have ocurred</returns>
        Task<IResponse<T>> Post<T>(string address, Dictionary<string, string> parameters = null,
            Dictionary<string, string> extraHeaders = null, object body = null);

        /// <summary>
        /// Do a http POST to the specified address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="parameters">The parameters for query string.</param>
        /// <param name="extraHeaders">Any extra http headers to send with the request</param>
        /// <param name="body">The body.</param>
        /// <returns>The response from Judy Pay with information of errors if they have ocurred</returns>
        Task<IResponse> Post(string address, Dictionary<string, string> parameters = null,
            Dictionary<string, string> extraHeaders = null, object body = null);

        /// <summary>
        /// Do a http UPDATE to the specified address.
        /// </summary>
        /// <typeparam name="T">The type of response model</typeparam>
        /// <param name="address">The address.</param>
        /// <param name="parameters">The parameters for query string</param>
        /// <param name="extraHeaders">Any extra http headers to send with the request</param>
        /// <param name="body">The body.</param>
        /// <returns>The response from Judy Pay with information of errors if they have ocurred</returns>
        Task<IResponse<T>> Update<T>(string address, Dictionary<string, string> parameters = null,
            Dictionary<string, string> extraHeaders = null, object body = null);

        /// <summary>
        /// Do a http UPDATE to the specified address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="parameters">The parameters for query string</param>
        /// <param name="extraHeaders">Any extra http headers to send with the request</param>
        /// <param name="body">The body.</param>
        /// <returns>The response from Judy Pay with information of errors if they have ocurred</returns>
        Task<IResponse> Update(string address, Dictionary<string, string> parameters = null,
            Dictionary<string, string> extraHeaders = null, object body = null);

        /// <summary>
        /// Do a http Delete to the specified address.
        /// </summary>
        /// <typeparam name="T">The type of response model</typeparam>
        /// <param name="address">The address.</param>
        /// <param name="parameters">The parameters for query string</param>
        /// <param name="extraHeaders">Any extra http headers to send with the request</param>
        /// <returns>The response from Judy Pay with information of errors if they have ocurred</returns>
        Task<IResponse<T>> Delete<T>(string address, Dictionary<string, string> parameters = null,
            Dictionary<string, string> extraHeaders = null);

        /// <summary>
        /// Do a http Delete to the specified address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="parameters">The parameters for query string</param>
        /// <param name="extraHeaders">Any extra http headers to send with the request</param>
        /// <returns>The response from Judy Pay with information of errors if they have ocurred</returns>
        Task<IResponse> Delete(string address, Dictionary<string, string> parameters = null,
            Dictionary<string, string> extraHeaders = null);

        Connection Connection { get; }
    }
    // ReSharper restore UnusedMember.Global
}