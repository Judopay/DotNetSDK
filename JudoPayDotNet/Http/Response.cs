using System.Net;
using JudoPayDotNet.Errors;

namespace JudoPayDotNet.Http
{
    internal class Response : IResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ResponseBody { get; set; }
        public JudoApiErrorModel JudoError { get; set; }

        public bool ErrorResponse { get { return JudoError != null; } }
    }

    internal class Response<T> : Response, IResponse<T>
    {
        public T ResponseBodyObject { get; set; }
    }
}
