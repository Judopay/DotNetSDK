using System;
using System.Globalization;

namespace JudoPayDotNet.Enums
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class DescriptionAttribute : Attribute
    {
        public string Description { get; set; }

        public DescriptionAttribute(string description)
        {
            Description = description;
        }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class LocalizedDescriptionAttribute : DescriptionAttribute
    {
        private readonly CultureInfo _culture;

        public LocalizedDescriptionAttribute(string description, CultureInfo culture)
            : base(description)
        {
            _culture = culture;
        }

        public LocalizedDescriptionAttribute(string description, string cultureName)
            : base(description)
        {
            _culture = new CultureInfo(cultureName);
        }

        public LocalizedDescriptionAttribute(string description)
            : base(description)
        {
            _culture = CultureInfo.InvariantCulture;
        }

        public CultureInfo Culture
        {
            get { return _culture; }
        }
    }

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
