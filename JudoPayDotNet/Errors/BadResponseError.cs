using System;
using System.Net.Http;

namespace JudoPayDotNet.Errors
{
    [Serializable]
    public class BadResponseError : Error
    {
        public BadResponseError(HttpResponseMessage response)
        {
            ErrorMessage =
                String.Format("Response format isn't valid it should have been application/json but was {0}",
                    response.Content.Headers.ContentType.MediaType);
        }

        public BadResponseError(Exception e) : base(null, e)
        {
            ErrorMessage = e.Message;
        }
    }
}
