using System;
using System.Collections.Generic;
using System.Text;

namespace CamBuild.Core
{
	public class ConsoleLogger : ILogger
	{
		public ConsoleLogger()
		{
			this.Initialize();
		}

		private void Initialize()
		{
			Console.WriteLine("[" + Utility.FullDateTimestamp + "] CamBuild Console Logger Initialized" + Environment.NewLine);
		}

		public void Write(string type, string text)
		{
			string ex = "[" + type + "]";

			Console.Write("[" + Utility.Timestamp + "]" + ex.PadRight(30) + text + Environment.NewLine);
		}



	}
}
