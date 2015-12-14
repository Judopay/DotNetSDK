using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudoPayDotNet.Errors
{
    public class ModelError
    {
        public List<FieldError> ModelErrors { get; set; }

        public string Category { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
    }
}
