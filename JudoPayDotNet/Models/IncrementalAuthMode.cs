using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// A merchant initiated request to increase the amount of an existing PreAuth that was created
    /// with AllowIncrement=true
    /// </summary>
    [DataContract]
    public class IncrementalAuthModel : ReferencingTransactionBase
    {
        /// <summary>
        /// The merchant payment meta data.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public IDictionary<string, string> YourPaymentMetaData { get; set; }
    }
}
