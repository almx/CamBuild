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
	public class BuildFileElementExecutorTest
	{
		[Test]
		public void TestExecuteElements()
		{
			TestAction ta = new TestAction();
			ta.Fields.Add("Message", "Test message");

			MemoryLogger ml = new MemoryLogger();

			Logging.Instance.Loggers.Add(ml);

			List<IBuildFileElement> list = new List<IBuildFileElement>();
			list.Add(ta);

			new BuildFileElementExecutor().ExecuteElements(list);

			Assert.IsTrue(ml.Log.Contains("[ActionExecute] TestAction"));
		}
	}
}