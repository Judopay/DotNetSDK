using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JudoPayDotNet.Errors
{
    public class ConnectionError : Error
    {
        public ConnectionError(Exception e) : base(null , e)
        {
            ErrorMessage = e.Message;
        }
    }
}
