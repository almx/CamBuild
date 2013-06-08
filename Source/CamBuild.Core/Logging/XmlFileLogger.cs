using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace CamBuild.Core
{
	public class XmlFileLogger : ILogger
	{
		private string filePath;
		private XmlDocument xd;

		public XmlFileLogger(string filePath, string buildFileName)
		{
			if(File.Exists(filePath))
				File.Delete(filePath);

			this.filePath = filePath;

			this.InitializeXmlDocument(buildFileName);
		}

		private void InitializeXmlDocument(string buildFileName)
		{
			this.xd = new XmlDocument();
			this.xd.CreateXmlDeclaration("1.0", "utf-8", null);

			this.xd.AppendChild(this.xd.CreateElement("CamBuildLog"));
			this.xd.DocumentElement.SetAttribute("buildfile", buildFileName);
			this.xd.DocumentElement.SetAttribute("timestamp", DateTime.Now.ToString("s"));

			this.xd.Save(this.filePath);
		}

		public void Write(string type, string text)
		{
			XmlElement xe = (XmlElement)this.xd.DocumentElement.AppendChild(this.xd.CreateElement("Entry"));
			xe.SetAttribute("timestamp", Utility.FullDateTimestamp);
			xe.SetAttribute("type", type);

			xe.InnerText = text;

			this.xd.Save(this.filePath);
		}

	}
}
