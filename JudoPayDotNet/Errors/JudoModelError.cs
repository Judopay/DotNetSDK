using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudoPayDotNet.Errors
{
    public class JudoModelError
    {
        public string FieldName { get; set; }
        public string ErrorMessage { get; set; }
        public string DetailErrorMessage { get; set; }
    }
}
