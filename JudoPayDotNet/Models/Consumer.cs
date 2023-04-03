using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Details of your consumer as returned in receipts
    /// </summary>
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    [DataContract(Name = "Consumer", Namespace = "")]
    public class Consumer
    {
        /// <summary>
        /// The merchant consumer reference.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string YourConsumerReference { get; set; }
    }
    // ReSharper restore UnusedAutoPropertyAccessor.Global
}
