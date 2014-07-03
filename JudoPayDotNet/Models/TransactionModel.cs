using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudoPayDotNet.Models
{
    public class TransactionModel
    {
        public long TransactionId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }

        public DateTimeOffset AvailableDate { get; set; }

        /// <summary>
        /// Has the clearing time past for this transaction, and had it already been settled
        /// </summary>
        public bool IsAvailable { get; set; }
    }
}
