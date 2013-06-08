using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Xml;
using CamBuild.Core;
using CamBuild.Core.Exceptions;
using NUnit.Framework;
using BestBrains.System;

namespace CamBuild.Test
{
	[TestFixture]
	public class XmlFileLoggerTest
	{
		[Test]
		public void Test()
		{
			File.Delete(TestUtility.TempDir + @"TestLog.xml");

			XmlFileLogger logger = new XmlFileLogger(TestUtility.TempDir + @"TestLog.xml", "TestBuildFile");

			logger.Write("test", "test entry 1");
			logger.Write("test", "test entry 2");

			XmlDocument xd = new XmlDocument();
			xd.Load(TestUtility.TempDir + @"TestLog.xml");

			Assert.AreEqual("CamBuildLog", xd.DocumentElement.Name);
			Assert.AreEqual(2, xd.SelectNodes("//Entry").Count);
			Assert.AreEqual("test entry 1", xd.SelectNodes("//Entry")[0].InnerText);
			Assert.AreEqual("test entry 2", xd.SelectNodes("//Entry")[1].InnerText);

			File.Delete(TestUtility.TempDir + @"TestLog.xml");
		}
	}
}