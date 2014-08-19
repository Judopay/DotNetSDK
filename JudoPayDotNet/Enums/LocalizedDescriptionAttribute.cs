using System;
using System.Globalization;

namespace JudoPayDotNet.Enums
{
    // ReSharper disable UnusedMember.Global
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    internal class LocalizedDescriptionAttribute : DescriptionAttribute
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
    // ReSharper restore UnusedMember.Global
}