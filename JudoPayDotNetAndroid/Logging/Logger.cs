using System;
using JudoPayDotNet.Logging;

namespace JudoPayDotNetAndroid
{
	public class Logger : ILog
	{
		private readonly Serilog.ILogger _logger;

		public Logger (Serilog.ILogger logger)
		{
			_logger = logger;
		}

		public void Debug (object message)
		{
			_logger.Debug (message.ToString());
		}

		public void Debug (object message, Exception exception)
		{
			_logger.Debug (exception, message.ToString());
		}

		public void DebugFormat (string format, params object[] args)
		{
			_logger.Debug (format, args);
		}

		public void DebugFormat (string format, Exception exception, params object[] args)
		{
			_logger.Debug (exception, format, args);
		}

		public void DebugFormat (IFormatProvider formatProvider, string format, params object[] args)
		{
			var message = string.Format (formatProvider, format, args);
			_logger.Debug (message);
		}

		public void DebugFormat (IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			var message = string.Format (formatProvider, format, args);
			_logger.Debug (exception, message);
		}

		public void Info (object message)
		{
			_logger.Information (message.ToString ());
		}

		public void Info (object message, Exception exception)
		{
			_logger.Information (exception, message.ToString ());
		}

		public void InfoFormat (string format, params object[] args)
		{
			_logger.Information (format, args);
		}

		public void InfoFormat (string format, Exception exception, params object[] args)
		{
			_logger.Information (exception, format, args);
		}

		public void InfoFormat (IFormatProvider formatProvider, string format, params object[] args)
		{
			var message = string.Format (formatProvider, format, args);
			_logger.Information (message);
		}

		public void InfoFormat (IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			var message = string.Format (formatProvider, format, args);
			_logger.Information (exception, message);
		}

		public void Warn (object message)
		{
			_logger.Warning (message.ToString ());
		}

		public void Warn (object message, Exception exception)
		{
			_logger.Warning (exception, message.ToString ());
		}

		public void WarnFormat (string format, params object[] args)
		{
			_logger.Warning (format, args);
		}

		public void WarnFormat (string format, Exception exception, params object[] args)
		{
			_logger.Warning (exception, format, args);
		}

		public void WarnFormat (IFormatProvider formatProvider, string format, params object[] args)
		{
			var message = string.Format (formatProvider, format, args);
			_logger.Warning (message);
		}

		public void WarnFormat (IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			var message = string.Format (formatProvider, format, args);
			_logger.Warning (exception, message);
		}

		public void Error (object message)
		{
			_logger.Error (message.ToString ());
		}

		public void Error (object message, Exception exception)
		{
			_logger.Error (exception, message.ToString ());
		}

		public void ErrorFormat (string format, params object[] args)
		{
			_logger.Error (format, args);
		}

		public void ErrorFormat (string format, Exception exception, params object[] args)
		{
			_logger.Error (exception, format, args);
		}

		public void ErrorFormat (IFormatProvider formatProvider, string format, params object[] args)
		{
			var message = string.Format (formatProvider, format, args);
			_logger.Error (message);
		}

		public void ErrorFormat (IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			var message = string.Format (formatProvider, format, args);
			_logger.Error (exception, message);
		}

		public void Fatal (object message)
		{
			_logger.Fatal (message.ToString ());
		}

		public void Fatal (object message, Exception exception)
		{
			_logger.Fatal (exception, message.ToString ());
		}

		public void FatalFormat (string format, params object[] args)
		{
			_logger.Fatal (format, args);
		}

		public void FatalFormat (string format, Exception exception, params object[] args)
		{
			_logger.Fatal (exception, format, args);
		}

		public void FatalFormat (IFormatProvider formatProvider, string format, params object[] args)
		{
			var message = string.Format (formatProvider, format, args);
			_logger.Fatal (message);
		}

		public void FatalFormat (IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			var message = string.Format (formatProvider, format, args);
			_logger.Fatal (exception, message);
		}

		public bool IsDebugEnabled {
			get {
				return _logger.IsEnabled(Serilog.Events.LogEventLevel.Debug);
			}
		}

		public bool IsErrorEnabled {
			get {
				return _logger.IsEnabled(Serilog.Events.LogEventLevel.Error);
			}
		}

		public bool IsFatalEnabled {
			get {
				return _logger.IsEnabled(Serilog.Events.LogEventLevel.Fatal);
			}
		}

		public bool IsInfoEnabled {
			get {
				return _logger.IsEnabled(Serilog.Events.LogEventLevel.Information);
			}
		}

		public bool IsWarnEnabled {
			get {
				return _logger.IsEnabled(Serilog.Events.LogEventLevel.Warning);
			}
		}
	}
}

