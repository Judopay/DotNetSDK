using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudoPayDotNet.Autentication
{
    public interface ICredentials
    {
        string Token { get; set; }
        string Secret { get; set; }
    }

    public class Credentials : ICredentials
    {
        public string Token { get; set; }
        public string Secret { get; set; }

        public Credentials(string token, string secret)
        {
            Token = token;
            Secret = secret;
        }
    }
}
