using System;
using System.Collections.Generic;
using System.Text;

namespace CamBuild.Console.Exceptions
{
	public class CamBuildConsoleException : Exception
	{
		private string message;

		public override string Message
		{
			get	{ return this.message; }
		}

		public CamBuildConsoleException(string message)
		{
			this.message = message;
		}

	}
}
