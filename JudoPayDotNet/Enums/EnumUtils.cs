using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace JudoPayDotNet.Enums
{
    // ReSharper disable UnusedMember.Global
    public static class EnumUtils
    {
        public static string GetDescriptionFromEnumValue(Enum value)
        {
            var attribute = value.GetType().
                GetRuntimeField(value.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .SingleOrDefault() as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static IEnumerable<string> EnumerateDescriptions(Type type)
        {
            if (!type.GetTypeInfo().IsEnum)
                throw new ArgumentException("This only works on enums", "type");

            return (from object value in Enum.GetValues(type)
                    select GetEnumDescription((Enum)value)).ToList();
        }

        public static string GetEnumDescription(Enum value)
        {
            var fi = value.GetType().GetRuntimeField(value.ToString());

            var attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false)
                                    .OfType<DescriptionAttribute>()
                                    .Where(a => a.GetType() == typeof(DescriptionAttribute));

            if (attributes != null && attributes.Count() > 0)
                return attributes.First().Description;

            var localizedAttributes = fi.GetCustomAttributes(typeof(LocalizedDescriptionAttribute), false) as LocalizedDescriptionAttribute[];

            if (localizedAttributes != null && localizedAttributes.Length > 0)
            {
                // match to the localized culture before invariants
                var matchingAttribute = localizedAttributes.FirstOrDefault(desc => desc.Culture.Equals(CultureInfo.CurrentUICulture) && !desc.Culture.Equals(CultureInfo.InvariantCulture));
                if (matchingAttribute != null)
                    return matchingAttribute.Description;

                var invariantFallback = localizedAttributes.FirstOrDefault(desc => desc.Culture.Equals(CultureInfo.InvariantCulture));
                if (invariantFallback != null)
                    return invariantFallback.Description;
            }

            return value.ToString();
        }

        public static string GetEnumExplanation(Enum value)
        {
            var fi = value.GetType().GetRuntimeField(value.ToString());

            var attributes = fi.GetCustomAttributes(typeof(ExplanationAttribute), false) as ExplanationAttribute[];

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Explanation;

            return string.Empty;
        }

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
