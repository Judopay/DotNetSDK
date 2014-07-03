using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudoPayDotNet.Models
{
    public class CardDetails
    {
        public string CardLastfour { get; set; }

        public string EndDate { get; set; }

        public string CardToken { get; set; }

        public CardType CardType { get; set; }
    }
}
