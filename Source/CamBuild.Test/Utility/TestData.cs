using System;
using System.Collections.Generic;
using System.Text;
using BestBrains.System;
using System.Xml;
using CamBuild.Core;

namespace CamBuild.Test
{
	public class TestData
	{
		///	<sample>
		///		<CamBuildProject name="Test Project" defaultcomponents=" Core , TestComponent">
		///			<Property name="OutputDir">C:\Temp\TestTool</Property>
		///			<WriteConsole>
		///				<Message>First test message</Message>
		///				<AddNewline>true</AddNewline>
		///			</WriteConsole>
		///			<BuildComponent name="Core">
		///		    </BuildComponent>
		///		    <BuildComponent name="TestComponent" dependencies="Core" description="Builds a test component">
		///				<WriteConsole>
		///					<Message>Current date/time: [$DateTime("YYYY-MM-DD-hh:mm:ss")]</Message>
		///				</WriteConsole>
		///			</BuildComponent>
		///		</CamBuildProject>
		///	</sample>
		public static string String()
		{
			return CommentReader.GetElement("sample");
		}

		public static BuildFile BuildFile
		{
			get
			{
				BuildFile bf = new BuildFile();
				bf.LoadXmlString(TestData.String());

				return bf;
			}
		}

		public static XmlDocument XmlDocument
		{
			get
			{
				XmlDocument xd = new XmlDocument();
				xd.LoadXml(TestData.String());

				return xd;
			}
		}

		public static XmlElement XmlElement
		{
			get
			{
				XmlDocument xd = new XmlDocument();
				xd.LoadXml(TestData.String());

				return (XmlElement)xd.GetElementsByTagName("CamBuildProject")[0];
			}
		}

	}
}