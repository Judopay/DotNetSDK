using System;

namespace JudoPayDotNet.Enums
{
	/// <summary>
	/// Provides programmatically accessable description of an enumeration value.
	/// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	internal class DescriptionAttribute : Attribute
    {
        public string Description { get; private set; }

        public DescriptionAttribute(string description)
        {
            Description = description;
        }
    }
}
