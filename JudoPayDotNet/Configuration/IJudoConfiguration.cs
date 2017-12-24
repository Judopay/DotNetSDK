namespace JudoPayDotNet.Configuration
{
    /// <summary>
    /// Defines a wrapping for configuration settings. This enables decoupling from ConfigurationManager and subsequent mocking of it.
    /// </summary>
    public interface IJudoConfiguration
    {
        /// <summary>
        /// Gets the configuration setting with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The configuration setting</returns>
        string this[string key] { get; }
    }
}