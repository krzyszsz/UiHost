using log4net;
using Prism.Logging;

namespace CommonLib.Logging
{
	public class Log4NetLogger : ILoggerFacade
	{
		private readonly ILog _logger = LogManager.GetLogger(typeof(Log4NetLogger));

		public void Log(string message, Category category = Category.Info, Priority priority = Priority.None)
		{
			switch (category)
			{
				case Category.Debug:
					_logger.Debug(message);
					break;
				case Category.Warn:
					_logger.Warn(message);
					break;
				case Category.Exception:
					_logger.Error(message);
					break;
				case Category.Info:
					_logger.Info(message);
					break;
			}
		}
	}
}