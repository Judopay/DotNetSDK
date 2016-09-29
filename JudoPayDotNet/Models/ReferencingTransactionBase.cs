using System.Collections.Generic;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// The base class for transactions that reference other transactions (like voids, collections and refunds)
    /// </summary>
    public abstract class ReferencingTransactionBase :  IModelWithHttpHeaders
    {
        private Dictionary<string, string> _httpHeaders;

        /// <summary>
        /// Allows you to set HTTP headers on requests
        /// </summary>
        public Dictionary<string, string> HttpHeaders
        {
            get { return _httpHeaders ?? (_httpHeaders = new Dictionary<string, string>()); }
        }
    }
}