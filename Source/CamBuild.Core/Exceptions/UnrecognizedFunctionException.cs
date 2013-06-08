using System;
using System.Collections.Generic;
using System.Text;

namespace CamBuild.Core.Exceptions
{
	[Serializable]
	public class UnrecognizedFunctionException : Exception
	{
		private string message;
		private string functionName;

		public string FunctionName
		{
			get { return functionName; }
			set { functionName = value; }
		}

		public override string Message
		{
			get
			{
				return message;
			}
		}

		public UnrecognizedFunctionException(string message)
		{
			this.message = message;
		}

		public UnrecognizedFunctionException(string functionName, string message)
		{
			this.message = message;
			this.functionName = functionName;
		}

	}
}
