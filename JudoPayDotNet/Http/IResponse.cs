using System.Net;
using JudoPayDotNet.Errors;

namespace JudoPayDotNet.Http
{
    /// <summary>
    /// The type of response model
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IResponse<T>
    {
        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the response body object.
        /// </summary>
        /// <value>
        /// The response body object.
        /// </value>
        T ResponseBodyObject { get; set; }

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
