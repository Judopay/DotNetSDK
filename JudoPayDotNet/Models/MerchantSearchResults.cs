using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudoPayDotNet.Models
{
    public class MerchantSearchResults
    {
        public long ResultCount { get; set; }

        public long PageSize { get; set; }

        public long Offset { get; set; }

        public IEnumerable<MarketPlaceMerchant> Results { get; set; }

        public string Sort { get; set; }
    }
}
