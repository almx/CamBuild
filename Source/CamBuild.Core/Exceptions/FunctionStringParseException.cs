using System;
using System.Collections.Generic;
using System.Text;

namespace CamBuild.Core.Exceptions
{
	[Serializable]
	public class FunctionStringParseException : Exception
	{
		private string message;
		private string functionString;

		public string FunctionString
		{
			get { return functionString; }
			set { functionString = value; }
		}

		public override string Message
		{
			get
			{
				return message;
			}
		}

		public FunctionStringParseException(string message)
		{
			this.message = message;
		}

		public FunctionStringParseException(string message, string functionString)
		{
			this.message = message;
			this.functionString = functionString;
		}
	}
}
