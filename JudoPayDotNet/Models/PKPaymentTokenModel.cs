using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace JudoPayDotNet.Models
{
    [DataContract]
    public class PKPaymentTokenModel
    {

        /// <summary>
        /// Gets or sets the address line1.
        /// </summary>
        /// <value>
        /// The PaymentInstrumentName.
        /// </value>
        [DataMember(IsRequired = true)]
        public string PaymentInstrumentName { get; set; }

        /// <summary>
        /// Gets or sets the address line2.
        /// </summary>
        /// <value>
        /// The PaymentNetwork.
        /// </value>
        [DataMember(IsRequired = true)]
        public string PaymentNetwork { get; set; }

        /// <summary>
        /// Gets or sets the address line3.
        /// </summary>
        /// <value>
        /// The PaymentData.
        /// </value>
        [DataMember(IsRequired = true)]
        public JObject PaymentData { get; set; }
    }
}
