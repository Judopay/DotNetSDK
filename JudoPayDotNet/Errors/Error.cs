using System;

namespace JudoPayDotNet.Errors
{
    public abstract class Error : Exception
    {
        protected string ErrorMessage { get; set; }

        public override string Message
        {
            get { return ErrorMessage; }
        }

        protected Error()
        { }

        protected Error(string message, Exception exception) : base(message, exception)
        { }
    }
}
