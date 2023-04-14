using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Collect all or part of a previously authorised (PreAuth) transaction
    /// </summary>
    [DataContract]
    public class CollectionModel : ReferencingTransactionBase
    {
        /// <summary>
        /// The merchant payment meta data.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public IDictionary<string, string> YourPaymentMetaData { get; set; }
    }
}
