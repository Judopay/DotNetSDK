using System.Configuration;

namespace JudoPayDotNet.Configuration
{
    /// <summary>
    /// Default implementation of configuration settings that uses ConfigurationManager.AppSettings
    /// </summary>
    public class JudoConfiguration : IJudoConfiguration
    {
        /// <summary>
        /// Gets the configuration setting with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The configuration setting</returns>
        public string this[string key]
        {
            get { return ConfigurationManager.AppSettings[key]; }
        }
    }
}
