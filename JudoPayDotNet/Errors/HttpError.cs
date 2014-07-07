using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JudoPayDotNet.Errors
{
    public class HttpError : Error
    {
        public string StatusCode { get; private set; }

        public HttpError(HttpResponseMessage response)
        {
            StatusCode = response.StatusCode.ToString();

            ErrorMessage = String.Format("Status Code : {0}, with content: {1}", 
                StatusCode,                             
                response.Content.ReadAsStringAsync().Result);
        }
    }
}
