using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudoPayDotNet.Models
{
    public class ApplicationPrincipleModel
    {
        public string Salutation { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DateOfBirth { get; set; }

        public string MobileNumber { get; set; }

        public string HomePhone { get; set; }

        public ApplicationAddressModel CurrentAddress { get; set; }

        public int? MonthsAtAddress { get; set; }
        public List<ApplicationAddressModel> PreviousAddresses { get; set; }

        public string EmailAddress { get; set; }

        public string Nationality { get; set; }

        public long ApplicationPrincipleRecId { get; set; }

        public bool IsOtherOwner { get; set; }

        public long? AccountPrincipleRecId { get; set; }
    }
}
