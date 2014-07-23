using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    [DataContract(Name = "ApplicationPrinciple", Namespace = "")]
    // ReSharper disable UnusedMember.Global
// ReSharper disable ClassNeverInstantiated.Global
    public class ApplicationPrincipleModel
// ReSharper restore ClassNeverInstantiated.Global
    {
        /// <summary>
        /// Gets or sets the salutation.
        /// </summary>
        /// <value>
        /// The salutation.
        /// </value>
        [DataMember(EmitDefaultValue = false)]

        public string Salutation { get; set; }


        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        /// <value>
        /// The date of birth.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the mobile number.
        /// </summary>
        /// <value>
        /// The mobile number.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string MobileNumber { get; set; }

        /// <summary>
        /// Gets or sets the home phone.
        /// </summary>
        /// <value>
        /// The home phone.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string HomePhone { get; set; }

        /// <summary>
        /// Gets or sets the current address.
        /// </summary>
        /// <value>
        /// The current address.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public ApplicationAddressModel CurrentAddress { get; set; }

        /// <summary>
        /// Gets or sets the months at address.
        /// </summary>
        /// <value>
        /// The months at address.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public int? MonthsAtAddress { get; set; }

        /// <summary>
        /// Gets or sets the previous addresses.
        /// </summary>
        /// <value>
        /// The previous addresses.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public List<ApplicationAddressModel> PreviousAddresses { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>
        /// The email address.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the nationality.
        /// </summary>
        /// <value>
        /// The nationality.
        /// </value>
        public string Nationality { get; set; }

        /// <summary>
        /// Gets or sets the application principle record identifier.
        /// </summary>
        /// <value>
        /// The application principle record identifier.
        /// </value>
        public long ApplicationPrincipleRecId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether other is owner.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is other owner; otherwise, <c>false</c>.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public bool IsOtherOwner { get; set; }

        /// <summary>
        /// Gets or sets the account principle record identifier.
        /// </summary>
        /// <value>
        /// The account principle record identifier.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public long? AccountPrincipleRecId { get; set; }
    }
    // ReSharper restore UnusedMember.Global
}
