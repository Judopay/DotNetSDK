using System;

namespace JudoPayDotNet.Enums
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ExplanationAttribute : Attribute
    {
        private readonly string _whatsGoingOn;

        public ExplanationAttribute(string whatsGoingOn)
        {
            _whatsGoingOn = whatsGoingOn;
        }

        public string Explanation
        {
            get
            {
                return _whatsGoingOn;
            }
        }
    }
}