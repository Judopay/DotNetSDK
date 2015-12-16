using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudoPayDotNet.Errors
{
    public class FieldError
    {

        public int Code { get; set; }
        public String FieldName { get; set; }
        public String Message { get; set; }
        public String Detail { get; set; }

        public FieldError(int _code, String _fieldName, String _message, String _detail)
        {
            Code = _code;
            FieldName =_fieldName;
            Message = _message;
            Detail = _detail;
        }

        public FieldError()
        {
            
        }


    }
}