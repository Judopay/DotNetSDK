using System;

namespace JudoPayDotNet.Enums
{
	/// <summary>
	/// An attribute for enumeration values, used to explain the reason for error messages
	/// </summary>
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