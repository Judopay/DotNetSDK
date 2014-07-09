using System;
using Serilog;
using JudoPayDotNet.Logging;

namespace JudoPayDotNetAndroid.Logging
{
	public class AndroidLoggerFactory
	{
		public static ILog Create(Type askingType)
		{
			return new Logger (new LoggerConfiguration().CreateLogger());
		}
	}
}

