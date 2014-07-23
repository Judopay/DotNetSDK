using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Application Business
    /// </summary>
    [DataContract(Name = "ApplicationBusiness", Namespace = "")]
    // ReSharper disable UnusedMember.Global
// ReSharper disable ClassNeverInstantiated.Global
    public class ApplicationBusinessModel
// ReSharper restore ClassNeverInstantiated.Global
    {
        /// <summary>
        /// Gets or sets the type of company.
        /// </summary>
        /// <value>
        /// The type of company.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string TypeOfCompany { get; set; }


        /// <summary>
        /// Gets or sets the name of the legal.
        /// </summary>
        /// <value>
        /// The name of the legal.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string LegalName { get; set; }

        /// <summary>
        /// Gets or sets the company number.
        /// </summary>
        /// <value>
        /// The company number.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string CompanyNumber { get; set; }

        /// <summary>
        /// Gets or sets the legal address.
        /// </summary>
        /// <value>
        /// The legal address.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public ApplicationAddressModel LegalAddress { get; set; }

        /// <summary>
        /// Gets or sets the years in business.
        /// </summary>
        /// <value>
        /// The years in business.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public int? YearsInBusiness { get; set; }
    }
    // ReSharper restore UnusedMember.Global
}
