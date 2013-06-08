using System;
using System.Collections.Generic;
using System.Text;

namespace CamBuild.Core.Exceptions
{
	[Serializable]
	public class PropertyStringParseException : Exception
	{
		private string message;
		private string propertyString;

		public string PropertyString
		{
			get { return propertyString; }
			set { propertyString = value; }
		}
		
		public override string Message
		{
			get
			{
				return message;
			}
		}

		public PropertyStringParseException(string message)
		{
			this.message = message;
		}

		public PropertyStringParseException(string message, string propertyString)
		{
			this.message = message;
			this.propertyString = propertyString;
		}
	}
}
