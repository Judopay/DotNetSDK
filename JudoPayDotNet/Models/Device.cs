using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Details of the device being used in the requested operation (add card/payment)
    /// </summary>
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    [DataContract(Name = "Device", Namespace = "")]
    public class Device
    {
        /// <summary>
        /// Gets or sets the consumer token.
        /// </summary>
        /// <value>
        /// The consumer token.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string Identity { get; set; }

        // ReSharper restore UnusedAutoPropertyAccessor.Global
    }
}
