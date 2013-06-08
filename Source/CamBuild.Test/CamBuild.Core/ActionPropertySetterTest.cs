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
	public class ActionPropertySetterTest
	{
		[Test]
		public void SetPropertiesTest()
		{
			XmlElement xe = (XmlElement)TestData.XmlDocument.SelectSingleNode("/CamBuildProject/WriteConsole");

			IAction ba = ActionFactory.Create(xe);

			Assert.AreEqual("WriteConsole", ba.GetType().Name);
			Assert.IsTrue(ba.Fields.ContainsKey("Message"));
			Assert.IsNull(ba.GetType().GetProperty("Message").GetValue(ba, null));

			ActionPropertySetter.SetProperties(ba);

			Assert.IsFalse(ba.Fields.ContainsKey("Message"));
			Assert.AreEqual("First test message", ba.GetType().GetProperty("Message").GetValue(ba, null).ToString());

		}
	}
}