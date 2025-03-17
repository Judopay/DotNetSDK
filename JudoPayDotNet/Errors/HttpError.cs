using System.Net.Http;

namespace JudoPayDotNet.Errors
{
    public class HttpError : Error
    {
// ReSharper disable MemberCanBePrivate.Global
        public string StatusCode { get; }
// ReSharper restore MemberCanBePrivate.Global

        public HttpError(HttpResponseMessage response)
        {
            StatusCode = response.StatusCode.ToString();

            ErrorMessage = $"Status Code : {StatusCode}, with content: {response.Content.ReadAsStringAsync().Result}";
        }
    }
}
