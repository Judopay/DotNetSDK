using System.Collections.Generic;

namespace JudoPayDotNet.Errors
{
    public class ModelError
    {
        public List<FieldError> ModelErrors { get; set; }

        public string Category { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }

        public string RequestId { get; set; }
    }
}
