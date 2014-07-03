using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudoPayDotNet.Models
{
    public interface ITransactionResult
    {
        string ReceiptId { get; set; }

        string Result { get; set; }

        string Message { get; set; }
    }
}
