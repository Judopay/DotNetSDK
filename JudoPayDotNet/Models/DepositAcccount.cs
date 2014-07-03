using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudoPayDotNet.Models
{
    public class DepositAcccount
    {
        public string SortCode { get; set; }

        public int? MaskSortCode { get; set; }

        public string AccountNumber { get; set; }

        public string Reference { get; set; }

        public int PageSaved { get; set; }

        public string NameOnAccount { get; set; }
        public string IBAN { get; set; }
        public string SwiftCode { get; set; }
        public string NameOfBank { get; set; }
        public string BankAddressLine1 { get; set; }
        public string BankAddressLine2 { get; set; }
        public string BankAddressLine3 { get; set; }
        public string BankCity { get; set; }
        public string BankPostCode { get; set; }
        public string BankCountry { get; set; }
    }
}
