using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Generic information about a payment
    /// </summary>
    // ReSharper disable UnusedMember.Global
    [DataContract]
    public abstract class PaymentModel
    {
        protected PaymentModel()
        {
            Currency = "GBP";
        }

        /// <summary>
        /// Gets or sets your consumer reference.
        /// </summary>
        /// <value>
        /// Your consumer reference.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string YourConsumerReference { get; set; }

        /// <summary>
        /// Gets or sets your payment reference.
        /// </summary>
        /// <value>
        /// Your payment reference.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string YourPaymentReference { get; set; }

        /// <summary>
        /// Gets or sets your payment meta data.
        /// </summary>
        /// <value>
        /// Your payment meta data.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public IDictionary<string, string> YourPaymentMetaData { get; set; }

        /// <summary>
        /// Gets or sets the judo identifier.
        /// </summary>
        /// <value>
        /// The judo identifier.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string JudoId { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
// ReSharper disable MemberCanBePrivate.Global
        public string Currency { get; set; }
// ReSharper restore MemberCanBePrivate.Global

        /// <summary>
        /// Gets or sets the partner service fee.
        /// </summary>
        /// <value>
        /// The partner service fee.
        /// </value>
        [DataMember]
// ReSharper disable UnusedAutoPropertyAccessor.Global
        public decimal PartnerServiceFee { get; set; }
// ReSharper restore UnusedAutoPropertyAccessor.Global

        /// <summary>
        /// Gets or sets the CV2.
        /// </summary>
        /// <value>
        /// The CV2.
        /// </value>
        [DataMember(IsRequired = true)]
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable InconsistentNaming
        public string CV2 { get; set; }
// ReSharper restore InconsistentNaming
// ReSharper restore UnusedAutoPropertyAccessor.Global


        /// <summary>
        /// Gets or sets the consumer location.
        /// </summary>
        /// <value>
        /// The consumer location.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
// ReSharper disable UnusedAutoPropertyAccessor.Global
        public ConsumerLocationModel ConsumerLocation { get; set; }
// ReSharper restore UnusedAutoPropertyAccessor.Global

        /// <summary>
        /// Gets or sets the mobile number.
        /// </summary>
        /// <value>
        /// The mobile number.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
// ReSharper disable UnusedAutoPropertyAccessor.Global
        public string MobileNumber { get; set; }
// ReSharper restore UnusedAutoPropertyAccessor.Global

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>
        /// The email address.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
// ReSharper disable UnusedAutoPropertyAccessor.Global
        public string EmailAddress { get; set; }
// ReSharper restore UnusedAutoPropertyAccessor.Global

        /// <summary>
        /// This is a set of fraud signals sent by the mobile SDKs
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
// ReSharper disable once UnusedMember.Global
        public JObject ClientDetails { get; set; }

        /// <summary>
        /// The Client's browser useragent for 3D secure
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
// ReSharper disable UnusedAutoPropertyAccessor.Global
        public string UserAgent { get; set; }
// ReSharper restore UnusedAutoPropertyAccessor.Global

        /// <summary>
        /// The Client's browser DeviceCategory for 3D secure
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string DeviceCategory { get; set; }

        /// <summary>
        /// The Client's browser DeviceCategory for 3D secure
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
// ReSharper disable UnusedAutoPropertyAccessor.Global
        public string AcceptHeaders { get; set; }
// ReSharper restore UnusedAutoPropertyAccessor.Global
    }
    // ReSharper restore UnusedMember.Global
}
