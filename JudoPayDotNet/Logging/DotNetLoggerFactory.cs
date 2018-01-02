using System;
using log4net;
using ILog = JudoPayDotNet.Logging.ILog;

namespace JudoPayDotNet.Logging
{
	/// <summary>
	/// Creates a log4net instance wrapper in the judopay sdk ILog logger interface.
	/// </summary>
    public static class DotNetLoggerFactory
    {
		/// <summary>
		/// Create an instance of ILog
		/// </summary>
		/// <param name="askingType">the type requesting the logger</param>
		/// <returns></returns>
        public static ILog Create(Type askingType)
        {
            return new Logger(LogManager.GetLogger(askingType));
        }
    }
}
