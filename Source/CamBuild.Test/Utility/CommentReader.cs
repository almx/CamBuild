using System;
using System.Text;
using System.Xml;
using System.Diagnostics;
using System.Reflection;

namespace BestBrains.System
{
	public class CommentReader
	{
		private static XmlDocument generatedDocumentation = new XmlDocument();
		private static StackFrame callingMethodFrame;

		public static string GetElement(string elementName)
		{
			StackTrace stackTrace = new StackTrace();
			callingMethodFrame = stackTrace.GetFrame(1);

			generatedDocumentation.Load(String.Format("{0}.xml", 
				Assembly.GetCallingAssembly().FullName.Split(',')[0]));

			XmlNode node = generatedDocumentation.SelectSingleNode(
				String.Format("doc/members/member[contains(@name, '{0}.{1}')]/{2}",
				callingMethodFrame.GetMethod().DeclaringType.ToString(),
				callingMethodFrame.GetMethod().Name,
				elementName));

			if (node != null)
				return node.InnerXml;
			else
				throw new ApplicationException(String.Format("Element '{0}' not found.", elementName));
		}
	}
}
