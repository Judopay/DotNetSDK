using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// A deposit account 
    /// </summary>
    [DataContract(Namespace = "")]
    public class DepositAcccount
    {
        /// <summary>
        /// Gets or sets the sort code.
        /// </summary>
        /// <value>
        /// The sort code.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string SortCode { get; set; }

        /// <summary>
        /// Gets or sets the mask sort code.
        /// </summary>
        /// <value>
        /// The mask sort code.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public int? MaskSortCode { get; set; }

        /// <summary>
        /// Gets or sets the account number.
        /// </summary>
        /// <value>
        /// The account number.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string AccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the reference.
        /// </summary>
        /// <value>
        /// The reference.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string Reference { get; set; }

        /// <summary>
        /// Gets or sets the page saved.
        /// </summary>
        /// <value>
        /// The page saved.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public int PageSaved { get; set; }

        /// <summary>
        /// Gets or sets the name on account.
        /// </summary>
        /// <value>
        /// The name on account.
        /// </value>
        public string NameOnAccount { get; set; }
        /// <summary>
        /// Gets or sets the iban.
        /// </summary>
        /// <value>
        /// The iban.
        /// </value>
        // ReSharper disable once InconsistentNaming
        public string IBAN { get; set; }
        /// <summary>
        /// Gets or sets the swift code.
        /// </summary>
        /// <value>
        /// The swift code.
        /// </value>
        public string SwiftCode { get; set; }
        /// <summary>
        /// Gets or sets the name of bank.
        /// </summary>
        /// <value>
        /// The name of bank.
        /// </value>
        public string NameOfBank { get; set; }
        /// <summary>
        /// Gets or sets the bank address line1.
        /// </summary>
        /// <value>
        /// The bank address line1.
        /// </value>
        public string BankAddressLine1 { get; set; }
        /// <summary>
        /// Gets or sets the bank address line2.
        /// </summary>
        /// <value>
        /// The bank address line2.
        /// </value>
        public string BankAddressLine2 { get; set; }
        /// <summary>
        /// Gets or sets the bank address line3.
        /// </summary>
        /// <value>
        /// The bank address line3.
        /// </value>
        public string BankAddressLine3 { get; set; }
        /// <summary>
        /// Gets or sets the bank city.
        /// </summary>
        /// <value>
        /// The bank city.
        /// </value>
        public string BankCity { get; set; }
        /// <summary>
        /// Gets or sets the bank post code.
        /// </summary>
        /// <value>
        /// The bank post code.
        /// </value>
        public string BankPostCode { get; set; }
        /// <summary>
        /// Gets or sets the bank country.
        /// </summary>
        /// <value>
        /// The bank country.
        /// </value>
        public string BankCountry { get; set; }
    }
}
