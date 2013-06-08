using System;
using System.Collections.Generic;
using System.Text;

namespace CamBuild.Core
{
	public class MemoryLogger : ILogger
	{
		private string log;

		public string Log
		{
			get { return log; }
			set { log = value; }
		}
		
		public void Write(string type, string text)
		{
			this.log += "[" + type + "] " + text + Environment.NewLine;
		}

		
	}
}
