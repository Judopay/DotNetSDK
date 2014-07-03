using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using JudoPayDotNet.Errors;

namespace JudoPayDotNet.Http
{
    public interface IResponse<T>
    {
        HttpStatusCode StatusCode { get; set; }
        T ResponseBodyObject { get; set; }
        string ResponseBody { get; set; }
        JudoApiErrorModel JudoError { get; set; }

        bool ErrorResponse { get; }
    }
}
