using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudoPayDotNet.Models
{
    public class WebPaymentsRequestModel
    {
        public decimal Amount { get; set; }
        public WebPaymentCardAddress CardAddress { get; set; }
        public string ClientIpAddress { get; set; }
        public string ClientUserAgent { get; set; }
        public string CompanyName { get; set; }
        public string Currency { get; set; }
        public DateTimeOffset ExpiryDate { get; set; }
        public string JudoId { get; set; }
        public long PartnerRecId { get; set; }
        public decimal PartnerServiceFee { get; set; }
    }
}
