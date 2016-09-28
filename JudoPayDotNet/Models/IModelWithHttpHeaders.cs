using System.Collections.Generic;

namespace JudoPayDotNet.Models
{
    public interface IModelWithHttpHeaders
    {
        /// <summary>
        /// Optional extra http headers to include in the request
        /// </summary>
        Dictionary<string, string> HttpHeaders { get; }
    }
}