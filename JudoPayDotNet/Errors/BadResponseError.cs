using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JudoPayDotNet.Errors
{
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
