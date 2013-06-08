using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.IO;
using System.Reflection;
using System.Xml;
using CamBuild.Core;
using BestBrains.System;
using CamBuild.Core.Exceptions;

namespace CamBuild.Test
{
	[TestFixture]
	public class ActionFactoryTest
	{
		[Test]
		public void CreateTest()
		{
			XmlDocument xd = TestData.XmlDocument;

			XmlElement xeGood = (XmlElement)xd.SelectSingleNode("/CamBuildProject/WriteConsole");
			XmlElement xeMalformed = (XmlElement)xd.DocumentElement;

			try
			{
				ActionFactory.Create(xeMalformed);
			}
			catch (Exception ex)
			{
				Assert.IsTrue(ex is UnrecognizedActionException);
			}

			IAction ba = ActionFactory.Create(xeGood);

			Assert.AreEqual("WriteConsole", ba.GetType().Name);
			Assert.AreEqual("First test message", ba.Fields["Message"]);
		}

		
	}
}
