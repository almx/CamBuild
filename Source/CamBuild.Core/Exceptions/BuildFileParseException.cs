using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace CamBuild.Core.Exceptions
{
	[Serializable]
	public class BuildFileParseException : Exception
	{
		private XmlElement _xml;
		private string _message;

		public override string Message
		{
			get
			{
				return _message;
			}
		}

		public XmlElement Xml
		{
			get { return _xml; }
			set { _xml = value; }
		}

		public BuildFileParseException(string message)
		{
			_message = message;
		}

		public BuildFileParseException(string message, XmlElement xe)
		{
			_xml = xe;
			_message = message;
		}

	}
}
