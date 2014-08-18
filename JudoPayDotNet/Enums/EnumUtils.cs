using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace JudoPayDotNet.Enums
{
    // ReSharper disable UnusedMember.Global
	/// <summary>
	/// A set of helper extension methods for working with Enumerations
	/// </summary>
    public static class EnumUtils
    {
		/// <summary>
		/// Returns the value of the <see cref="DescriptionAttribute"/> associated with this enumeration value. 
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
        public static string GetDescriptionFromEnumValue(Enum value)
        {
            var attribute = value.GetType().
                GetRuntimeField(value.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .SingleOrDefault() as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }

		/// <summary>
		/// Returns all <see cref="DescriptionAttribute"/> values for an enumeration type
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
        public static IEnumerable<string> EnumerateDescriptions(Type type)
        {
            if (!type.GetTypeInfo().IsEnum)
                throw new ArgumentException("This only works on enums", "type");

            return (from object value in Enum.GetValues(type)
                    select GetEnumDescription((Enum)value)).ToList();
        }

		/// <summary>
		/// Returns the value of the first <see cref="DescriptionAttribute"/> or <see cref="LocalizedDescriptionAttribute"/> (for 
		/// the current culture) associated with this enumeration value. 
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
        public static string GetEnumDescription(Enum value)
        {
            var fi = value.GetType().GetRuntimeField(value.ToString());

            var attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false)
                                    .OfType<DescriptionAttribute>()
                                    .Where(a => a.GetType() == typeof(DescriptionAttribute));

            if (attributes != null && attributes.Any())
                return attributes.First().Description;

            var localizedAttributes = fi.GetCustomAttributes(typeof(LocalizedDescriptionAttribute), false) as LocalizedDescriptionAttribute[];

	        if (localizedAttributes == null || localizedAttributes.Length <= 0) 
				return value.ToString();

	        // match to the localized culture before invariants
	        var matchingAttribute = localizedAttributes.FirstOrDefault(description => description.Culture.Equals(CultureInfo.CurrentUICulture) && !description.Culture.Equals(CultureInfo.InvariantCulture));
	        if (matchingAttribute != null)
		        return matchingAttribute.Description;

			var invariantFallback = localizedAttributes.FirstOrDefault(description => description.Culture.Equals(CultureInfo.InvariantCulture));
	        if (invariantFallback != null)
		        return invariantFallback.Description;

	        return value.ToString();
        }

		/// <summary>
		/// Returns the first <see cref="ExplanationAttribute"/> on an enumeration value.
		/// </summary>
		/// <remarks>This is usually used by error enumerations to provide a user readable explanation for the error.</remarks>
		/// <param name="value"></param>
		/// <returns></returns>
        public static string GetEnumExplanation(Enum value)
        {
            var fi = value.GetType().GetRuntimeField(value.ToString());

            var attributes = fi.GetCustomAttributes(typeof(ExplanationAttribute), false) as ExplanationAttribute[];

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Explanation;

            return string.Empty;
        }

		/// <summary>
		/// returns an enumeration value from a string based on the <see cref="DescriptionAttribute"/> or <see cref="LocalizedDescriptionAttribute"/> value
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="description"></param>
		/// <returns></returns>
        public static T GetValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.GetTypeInfo().IsEnum)
                throw new InvalidOperationException();

            foreach (var field in type.GetRuntimeFields())
            {
                var descriptionAttributes = field.GetCustomAttributes(typeof(DescriptionAttribute)) as DescriptionAttribute[];
                if (descriptionAttributes != null)
                {
                    foreach (var descriptionAttribute in descriptionAttributes)
                    {
                        var localizedAttribute = descriptionAttribute as LocalizedDescriptionAttribute;
                        if (localizedAttribute != null)
                        {
                            if (String.Equals(localizedAttribute.Description, description, StringComparison.CurrentCultureIgnoreCase)
                                && (localizedAttribute.Culture.Equals(CultureInfo.CurrentUICulture) || localizedAttribute.Culture.Equals(CultureInfo.InvariantCulture)))
                            {
                                return (T)field.GetValue(null);
                            }
                        }
                        else
                        {
                            if (String.Equals(descriptionAttribute.Description, description, StringComparison.CurrentCultureIgnoreCase))
                                return (T)field.GetValue(null);
                        }
                    }
                }

                if (String.Equals(field.Name, description, StringComparison.CurrentCultureIgnoreCase))
                    return (T)field.GetValue(null);
            }

            return default(T);
        }
    }
    // ReSharper restore UnusedMember.Global
}
