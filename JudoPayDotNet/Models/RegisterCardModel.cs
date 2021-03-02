﻿using System.Runtime.Serialization;
using JudoPayDotNet.Enums;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Data to register a card (pre-auth with a pre-configured amount)
    /// </summary>
    [DataContract]
    [KnownType(typeof(RegisterEncryptedCardModel))]
    // ReSharper disable UnusedMember.Global
    public class RegisterCardModel : SaveCardModel
    {
        /// <summary>
        /// Gets or sets the full card holder name.
        /// </summary>
        /// <value>
        /// The card holder name.
        /// </value>
        [DataMember(IsRequired = false)]
        public string CardHolderName { get; set; }

        /// <summary>
        /// Gets or sets the mobile number.
        /// </summary>
        /// <value>
        /// The mobile number.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string MobileNumber { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>
        /// The email address.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Is this transaction the first transaction of a series (has continuous authority
        /// been granted to the merchant by the card holder).
        /// </summary>
        /// <remarks>Mastercard requires that when dealing with continuous authority
        /// payments this flag identifies the transaction where the card holder gave permission for
        /// repeat charges.</remarks>
        [DataMember(EmitDefaultValue = false)]
        public bool? InitialRecurringPayment { get; set; }

        /// <summary>
        /// Indicates that the transaction has been given recurring authorization from the consumer
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public bool? RecurringPayment { get; set; }

        /// <summary>
        /// Enum for Regular Payments (recurring)
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public RecurringPaymentType? RecurringPaymentType { get; set; }

        /// <summary>
        /// Receipt ID of original authenticated transaction (for recurring payments)
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string RelatedReceiptId { get; set; }

        /// <summary>
        /// Information needed for ThreeDSecure2 payments
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public ThreeDSecureTwoModel ThreeDSecure { get; set; }
    }
    // ReSharper restore UnusedMember.Global
}
