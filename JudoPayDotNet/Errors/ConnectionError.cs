using System;

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
