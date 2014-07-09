using System;
using JudoPayDotNet.Logging;
using log4net;
using ILog = JudoPayDotNet.Logging.ILog;

namespace JudoPayDotNetDotNet.Logging
{
    public static class DotNetLoggerFactory
    {
        public static ILog Create(Type askingType)
        {
            return new Logger(LogManager.GetLogger(askingType));
        }
    }
}
