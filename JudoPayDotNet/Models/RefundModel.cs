using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudoPayDotNet.Models
{
    public class RefundModel
    {
        public int ReceiptId { get; set; }
        public decimal Amount { get; set; }
        public string YourPaymentReference { get; set; }
    }
}
