using System;
using System.Collections.Generic;
using JudoPayDotNet.Enums;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Errors
{
    public class JudoApiErrorModel
    {
        public String ErrorMessage { get; set; }
        public List<JudoModelError> ModelErrors { get; set; }
        public JudoApiError ErrorType { get; set; }
    }
}

