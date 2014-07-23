using System;

namespace JudoPayDotNet.Errors
{
    public abstract class Error : Exception
    {

// ReSharper disable MemberCanBePrivate.Global
        protected string ErrorMessage { get; set; }
// ReSharper restore MemberCanBePrivate.Global

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
