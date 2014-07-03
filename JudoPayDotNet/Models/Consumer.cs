using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudoPayDotNet.Models
{
    /// <summary>
    ///     Details of the consumer used in the requested operation (add card/payment)
    /// </summary>
    public class Consumer
    {
        public string ConsumerToken { get; set; }

        public string YourConsumerReference { get; set; }
    }
}
