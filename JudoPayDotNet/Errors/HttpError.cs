using System;
using System.Net.Http;

namespace JudoPayDotNet.Errors
{
    public class HttpError : Error
    {
// ReSharper disable MemberCanBePrivate.Global
        public string StatusCode { get; private set; }
// ReSharper restore MemberCanBePrivate.Global

        public HttpError(HttpResponseMessage response)
        {
            StatusCode = response.StatusCode.ToString();

            ErrorMessage = String.Format("Status Code : {0}, with content: {1}", 
                StatusCode,                             
                response.Content.ReadAsStringAsync().Result);
        }
    }
}
