using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Properties used for MCC 6012 payments only
    /// </summary>
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    public class PrimaryAccountDetailsModel
    {
        /// <summary>
        /// Gets or sets the Name of the Primary Account Holder
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Account Number of the Primary Account Holder 
        /// </summary>
        [DataMember]
        public string AccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the Date Of Birth of the Primary Account Holder 
        /// Format should be YYYY-MM-DD
        /// </summary>
        [DataMember]
        public string DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the Post Code of the Primary Account Holder 
        /// </summary>
        [DataMember]
        public string PostCode { get; set; }
    }
}
