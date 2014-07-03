using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudoPayDotNet.Models
{
    public class ApplicationModel
    {
        public string ApplicationReference { get; set; }

        public DateTimeOffset? ApplicationSubmittedDate { get; set; }

        public DateTimeOffset? AbandonedDate { get; set; }

        public string PartnerApplicationReference { get; set; }

        public IDictionary<string, string> YourApplicationMetaData { get; set; }

        public List<ApplicationPrincipleModel> Principle { get; set; }

        public ApplicationBusinessModel Business { get; set; }

        public List<ApplicationTradingLocation> Locations { get; set; }

        public DepositAcccount DepositAccount { get; set; }

        public string Product { get; set; }

        public string Signage { get; set; }

        public long? AccountRecId { get; set; }

        public long? ProductRecId { get; set; }

        public long? SellerUserId { get; set; }

        public long? PartnerRecId { get; set; }

        /// <summary>
        /// if this is a market place signup
        /// </summary>
        public string OAuthClientId { get; set; }
        public string PartnerShortName { get; set; }
    }
}
