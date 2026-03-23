using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Properties used for MCC codes that need to provide AFT Recipient Information
    /// (e.g. MCC 4829, 6012, 6051, 6211, 6540)
    /// </summary>
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    public class AftRecipientInformationModel
    {
        /// <summary>
        /// First name of the account holder
        /// </summary>
        [DataMember]
        public string FirstName { get; set; }

        /// <summary>
        /// First line of the address of the account holder
        /// </summary>
        [DataMember]
        public string AddressLine1 { get; set; }

        /// <summary>
        /// ISO-3166 country code of the account holder's country
        /// Format should be YYYY-MM-DD
        /// </summary>
        [DataMember]
        public string CountryCode { get; set; }

        /// <summary>
        /// When countryCode is US or CA, this is the 2 character state code of the account holder's state.
        /// Not required for other countries
        /// </summary>
        [DataMember]
        public string State { get; set; }

        /// <summary>
        /// 2 characters code indicating the type of account.
        /// "00" : Other
        /// "01" : Routing transit number (RTN) + Bank Account Number (BAN)
        /// "02" : International bank account number (IBAN)
        /// "03" : Card account
        /// "06" : Bank account number (BAN) + Bank identification code (BIC), also known as a SWIFT code
        /// </summary>
        [DataMember]
        public string AccountType { get; set; }

        /// <summary>
        /// Unique account identifier  for the account type in question, as specified in the accountType description.
        /// For type 03 Card account use the first 6 and last 4 of the card number.
        /// </summary>
        [DataMember]
        public string AccountId { get; set; }
    }
}
