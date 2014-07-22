//namespace JudoPayDotNet.Utility
//{
//    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
//    public class LocalizedDescriptionAttribute : DescriptionAttribute
//    {
//        private readonly CultureInfo _culture;

//        public LocalizedDescriptionAttribute(string description, CultureInfo culture)
//            : base(description)
//        {
//            _culture = culture;
//        }

//        public LocalizedDescriptionAttribute(string description, string cultureName)
//            : base(description)
//        {
//            _culture = CultureInfo.GetCultureInfo(cultureName);
//        }

//        public LocalizedDescriptionAttribute(string description)
//            : base(description)
//        {
//            _culture = CultureInfo.InvariantCulture;
//        }

//        public CultureInfo Culture
//        {
//            get { return _culture; }
//        }
//    }
//}
