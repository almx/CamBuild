using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace CamBuild.Core
{
	public class Logging
	{
		private List<ILogger> loggers;
		private static Logging instance;

		public ICollection<ILogger> Loggers
		{
			get { return this.loggers; }
		}

		public Logging()
		{
			this.loggers = new List<ILogger>();
		}

		public static Logging Instance
		{
			get
			{
				if (Logging.instance == null)
					Logging.instance = new Logging();

				return Logging.instance;
			}
		}

		public void LogEntry(string entryType, string text)
		{
			foreach (ILogger logger in this.loggers)
			{
				logger.Write(entryType, text);
			}
		}

		public bool ContainsLogger(string loggerName)
		{
			foreach (ILogger logger in this.loggers)
			{
				if (logger is ConsoleLogger && loggerName == "console")
					return true;

				if (logger is XmlFileLogger && loggerName == "xml")
					return true;

				if (logger is MemoryLogger && loggerName == "memory")
					return true;
			}

			return false;
		}
	}
}
