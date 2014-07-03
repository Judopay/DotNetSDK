using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudoPayDotNet.Models
{
    public class MarketPlaceMerchant
    {
        public string PartnerReference { get; set; }

        public string MerchantLegalName { get; set; }

        public DateTimeOffset AccessGranted { get; set; }

        public DateTimeOffset LastTransaction { get; set; }

        public IEnumerable<MarketPlaceMerchantLocation> Locations { get; set; }

        public string Scopes { get; set; }
    }
}
