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
	public class ValueStringEvaluatorTest
	{
		[Test]
		public void EvaluateTest()
		{
			string result = ValueStringEvaluator.Evaluate("test [$TestFunction()]test - [#OutputDir]", TestData.BuildFile);

			Assert.AreEqual(@"test !testvalue!test - C:\Temp\TestTool", result);
		}

	}

	
}