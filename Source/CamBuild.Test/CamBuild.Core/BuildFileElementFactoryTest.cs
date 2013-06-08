using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using NUnit.Framework;
using CamBuild.Core;
using BestBrains.System;

namespace CamBuild.Test
{
	[TestFixture]
	public class BuildFileElementFactoryTest
	{
		[Test]
		public void Create()
		{
			XmlDocument xd = TestData.XmlDocument;
			BuildFile bf = new BuildFile();
			bf.LoadXmlDocument(xd);

			IBuildFileElement elementAction = BuildFileElementFactory.Create((XmlElement)xd.SelectSingleNode("/CamBuildProject/WriteConsole"), bf);
			Assert.IsTrue(elementAction.GetType().Name == "WriteConsole");
			Assert.AreEqual(elementAction.ParentBuildFile, bf);

			IBuildFileElement elementBuildComponent = BuildFileElementFactory.Create((XmlElement)xd.SelectSingleNode("/CamBuildProject/BuildComponent[@name='TestComponent']"), bf);
			Assert.IsTrue(elementBuildComponent is BuildComponent);
			Assert.AreEqual("TestComponent", ((BuildComponent)elementBuildComponent).Name);
			Assert.AreEqual(elementBuildComponent.ParentBuildFile, bf);

			IBuildFileElement elementProperty = BuildFileElementFactory.Create((XmlElement)xd.GetElementsByTagName("Property")[0], bf);
			Assert.AreEqual("Property", elementProperty.GetType().Name);
			Assert.AreEqual("OutputDir", elementProperty.GetType().GetProperty("Name").GetValue(elementProperty, null).ToString());
			Assert.AreEqual(elementProperty.ParentBuildFile, bf);
		}
	}
}