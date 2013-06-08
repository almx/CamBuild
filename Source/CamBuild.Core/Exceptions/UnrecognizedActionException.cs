using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace CamBuild.Core.Exceptions
{
	[Serializable]
	public class UnrecognizedActionException : Exception
	{
		private string message;
		private string actionName;
		private XmlElement xml;

		public XmlElement Xml
		{
			get { return xml; }
			set { xml = value; }
		}

		public string ActionName
		{
			get { return actionName; }
			set { actionName = value; }
		}

		public override string Message
		{
			get
			{
				return message;
			}
		}

		public UnrecognizedActionException(string message)
		{
			this.message = message;
		}

		public UnrecognizedActionException(string message, string actionName, XmlElement xe)
		{
			this.message = message;
			this.actionName = actionName;
			this.xml = xe;
		}
	}
}
