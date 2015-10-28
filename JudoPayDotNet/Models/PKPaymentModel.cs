using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JudoPayDotNet.Models
{
    [DataContract]
    public class PKPaymentModel : PaymentModel
    {
        /// <summary>
        /// Gets or sets the apple pay token.
        /// </summary>
        /// <value>
        /// The apple pay token.
        /// </value>
        [DataMember(IsRequired = true)]
        public PKPaymentInnerModel PkPayment { get; set; }
    }
}
