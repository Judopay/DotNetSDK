using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudoPayDotNet.Models
{
    public class RegisterCardModel
    {
        public string Cv2 { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public CardAddressModel CardAddress { get; set; }
        public string YourConsumerReference { get; set; }
    }
}
