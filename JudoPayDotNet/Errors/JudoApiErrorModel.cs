using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JudoPayDotNet.Errors
{
    public class JudoApiErrorModel
    {
        public String ErrorMessage { get; set; }
        public List<JudoModelError> ModelErrors { get; set; }
        public long ErrorType { get; set; }
    }
}
