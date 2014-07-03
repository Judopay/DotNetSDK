using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudoPayDotNet.Models
{
    public class ApplicationTradingLocation
    {
        public string PartnerReference { get; set; }

        public string TradingName { get; set; }

        public ApplicationAddressModel TradingAddress { get; set; }

        public string LocationAnnualTurnover { get; set; }

        public string AverageTransactionValue { get; set; }

        public string MccCode { get; set; }

        public int JudoId { get; set; }

        public string TradingPhoneNumber { get; set; }

        public long ApplicationLocationLnkRecId { get; set; }

        public long? SignageTypeRecId { get; set; }

        public long? PhysicalGoodsLeadtimeRecId { get; set; }

        public string WebsiteAddress { get; set; }

        public string ProductDescription { get; set; }

        public string BusinessSupportNumber { get; set; }

        public long? AccountLocationLnkRecId { get; set; }
    }
}
