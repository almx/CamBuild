using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using NUnit.Framework;
using CamBuild.Core;
using BestBrains.System;
using CamBuild.Core.Exceptions;

namespace CamBuild.Test
{
	[TestFixture]
	public class BuildComponentTest
	{
		[Test]
		public void TestConstructorXmlParsing()
		{
			XmlDocument xd = TestData.XmlDocument;
			BuildFile bf = new BuildFile();
			bf.LoadXmlDocument(xd);

			try
			{
				new BuildComponent(xd.DocumentElement, bf);
			}
			catch (Exception ex)
			{
				Assert.IsTrue(ex is BuildFileParseException);
			}
		}

		[Test]
		public void TestActionsProperty()
		{
			XmlDocument xd = TestData.XmlDocument;
			BuildFile bf = new BuildFile();
			bf.LoadXmlDocument(xd);
			
			BuildComponent bc = new BuildComponent((XmlElement)xd.SelectSingleNode("//BuildComponent[@name='TestComponent']"), bf);

			List<IAction> actions = (List<IAction>)bc.Actions;

			Assert.IsTrue(actions.Count == 1);
			Assert.IsTrue(((IAction)actions[0]).GetType().Name == "WriteConsole");
		}

		[Test]
		public void TestNameProperty()
		{
			XmlDocument xd = TestData.XmlDocument;
			BuildFile bf = new BuildFile();
			bf.LoadXmlDocument(xd);

			BuildComponent bc = new BuildComponent((XmlElement)xd.SelectSingleNode("//BuildComponent[@name='TestComponent']"), bf);

			Assert.AreEqual(bc.Name, "TestComponent");
		}

		[Test]
		public void TestDependenciesProperty()
		{
			XmlDocument xd = TestData.XmlDocument;
			BuildFile bf = new BuildFile();
			bf.LoadXmlDocument(xd);

			BuildComponent bc = new BuildComponent((XmlElement)xd.SelectSingleNode("//BuildComponent[@name='TestComponent']"), bf);

			Assert.AreEqual(1, bc.Dependencies.Count);
			Assert.AreEqual(((List<string>)bc.Dependencies)[0], "Core");

			BuildComponent bc2 = new BuildComponent((XmlElement)xd.SelectSingleNode("//BuildComponent[@name='Core']"), bf);
			Assert.AreEqual(0, bc2.Dependencies.Count);

		}

	}
}