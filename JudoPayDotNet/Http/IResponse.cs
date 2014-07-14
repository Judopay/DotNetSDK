using System.Net;
using JudoPayDotNet.Errors;

namespace JudoPayDotNet.Http
{
    /// <summary>
    /// A response typefied
    /// </summary>
    /// <typeparam name="T">The type of response model</typeparam>
    public interface IResponse<T> : IResponse
    {
        /// <summary>
        /// Gets or sets the response body object.
        /// </summary>
        /// <value>
        /// The response body object.
        /// </value>
        T ResponseBodyObject { get; set; }
    }

    public interface IResponse
    {
        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the response body.
        /// </summary>
        /// <value>
        /// The response body.
        /// </value>
        string ResponseBody { get; set; }

        /// <summary>
        /// Gets or sets the judo error.
        /// </summary>
        /// <value>
        /// The judo error.
        /// </value>
        JudoApiErrorModel JudoError { get; set; }

        /// <summary>
        /// Gets a value indicating whether [error response].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [error response]; otherwise, <c>false</c>.
        /// </value>
        bool ErrorResponse { get; }
    }
}
