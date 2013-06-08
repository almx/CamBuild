using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using CamBuild.Core;
using BestBrains.System;
using System.Xml;

namespace CamBuild.Test
{
	[TestFixture]
	public class BuildFileElementParserTest
	{
		[Test]
		public void ParseTest()
		{
			XmlDocument xd = TestData.XmlDocument;
			BuildFile bf = new BuildFile();
			bf.LoadXmlDocument(xd);

			List<IBuildFileElement> elements = (List<IBuildFileElement>)BuildFileElementParser.Parse(xd, bf);

			Assert.AreEqual(4, elements.Count);
			Assert.AreEqual(typeof(Property), elements[0].GetType());
			Assert.AreEqual("WriteConsole", elements[1].GetType().Name);

			Assert.AreEqual(typeof(BuildComponent), elements[2].GetType());
			Assert.AreEqual("Core", ((BuildComponent)elements[2]).Name);


			Assert.AreEqual(typeof(BuildComponent), elements[3].GetType());
			Assert.AreEqual("TestComponent", ((BuildComponent)elements[3]).Name);

		}

	}
}
