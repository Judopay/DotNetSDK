using JudoPayDotNet.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudoPayDotNetWindowsPhone.Logging
{
    public static class WindowsPhoneLoggerFactory
    {
        public static ILog Create(Type requestingType)
        {
            var logger = new LoggerConfiguration().CreateLogger();
            return new Logger(logger);
        }
    }
}
