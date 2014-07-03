using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudoPayDotNet.Models
{
    public class ThreeDSecureReceiptModel
    {
        /// <summary>
        /// Did the consumer attempt to authenticate through 3d secure
        /// </summary>
        public bool Attempted { get; set; }


        /// <summary>
        /// what was the outcome of their authentication
        /// </summary>
        public string Result { get; set; }
    }
}
