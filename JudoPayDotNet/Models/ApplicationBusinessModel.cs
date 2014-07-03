using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudoPayDotNet.Models
{
    public class ApplicationBusinessModel
    {
        public string TypeOfCompany { get; set; }

        public string LegalName { get; set; }

        public string CompanyNumber { get; set; }

        public ApplicationAddressModel LegalAddress { get; set; }

        public int? YearsInBusiness { get; set; }
    }
}
