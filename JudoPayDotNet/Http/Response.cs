using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using JudoPayDotNet.Errors;

namespace JudoPayDotNet.Http
{
    internal class Response<T> : IResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public T ResponseBodyObject { get; set; }
        public string ResponseBody { get; set; }
        public JudoApiErrorModel JudoError { get; set; }

        public bool ErrorResponse { get { return JudoError != null; } }
    }
}
