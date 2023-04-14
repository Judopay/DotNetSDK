using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    [DataContract]
    public class ApplePayCardAddressModel
    {
        [DataMember(IsRequired = false)]
        public IEnumerable<string> AddressLines { get; set; }

        [DataMember(IsRequired = false)]
        public string AdministrativeArea { get; set; }

        [DataMember(IsRequired = false)]
        public string CountryCode { get; set; }

        [DataMember(IsRequired = false)]
        public string Locality { get; set; }

        [DataMember(IsRequired = false)]
        public string PostalCode { get; set; }
    }
}