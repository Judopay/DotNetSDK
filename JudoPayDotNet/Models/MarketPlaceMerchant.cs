using System;
using System.Collections.Generic;

namespace JudoPayDotNet.Models
{
    // ReSharper disable UnusedMember.Global
    // ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global
    public class MarketPlaceMerchant
// ReSharper restore ClassNeverInstantiated.Global
    {
        public string PartnerReference { get; set; }

        public string MerchantLegalName { get; set; }

        public DateTimeOffset AccessGranted { get; set; }

        public DateTimeOffset LastTransaction { get; set; }

        public IEnumerable<MarketPlaceMerchantLocation> Locations { get; set; }

        public string Scopes { get; set; }
    }
    // ReSharper restore UnusedMember.Global
    // ReSharper restore UnusedAutoPropertyAccessor.Global
}
