using System;
using System.Collections.Generic;
using System.Text;

namespace CamBuild.Core.Exceptions
{
	[Serializable]
	public class ValueStringEvaluatorException : Exception
	{
		private string message;
		private string valueString;

		public string ValueString
		{
			get { return valueString; }
			set { valueString = value; }
		}

		public override string Message
		{
			get
			{
				return message;
			}
		}

		public ValueStringEvaluatorException(string message)
		{
			this.message = message;
		}

		public ValueStringEvaluatorException(string message, string valueString)
		{
			this.message = message;
			this.valueString = valueString;
		}
	}
}
