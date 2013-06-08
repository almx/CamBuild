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
	public class ScenarioTestTool
	{
		[Ignore]
		public void RunBuildScriptTestTool()
		{
			BuildFile bf = new BuildFile();
			bf.LoadXmlFile(@"..\..\Scenario\TestTool.xml");

			if (Directory.Exists(TestUtility.TempDir + @"TestTool"))
				Directory.Delete(TestUtility.TempDir + @"TestTool", true);

			List<IBuildFileElement> elements = (List<IBuildFileElement>)bf.RootElements;
			new BuildFileElementExecutor().ExecuteElements(elements);

			Assert.IsTrue(File.Exists(TestUtility.TempDir + @"TestTool\TestApplication.exe"));
			Assert.IsTrue(File.Exists(TestUtility.TempDir + @"TestTool\TestLibrary.dll"));
			Assert.IsTrue(File.Exists(TestUtility.TempDir + @"TestTool\Manual\Manual.doc"));
			Assert.IsTrue(File.Exists(TestUtility.TempDir + @"TestTool\JavaApp.class"));

			Directory.Delete(TestUtility.TempDir + @"TestTool", true);
		}

		[Ignore]
		public void RunBuildScriptTestToolSpecificComponent()
		{
			BuildFile bf = new BuildFile();
			bf.LoadXmlFile(@"..\..\Scenario\TestTool.xml");

			if (Directory.Exists(TestUtility.TempDir + @"TestTool"))
				Directory.Delete(TestUtility.TempDir + @"TestTool", true);

			List<IBuildFileElement> elements = (List<IBuildFileElement>)bf.GetBuildComponentWithRootActions("TestLibrary");

			new BuildFileElementExecutor().ExecuteElements(elements);

			Assert.IsTrue(File.Exists(TestUtility.TempDir + @"TestTool\TestLibrary.dll"));
			Assert.IsTrue(!File.Exists(TestUtility.TempDir + @"TestTool\TestApplication.exe"));

			elements.Clear();
			elements.Add(bf.GetBuildComponent("JavaApp"));

			new BuildFileElementExecutor().ExecuteElements(elements);

			Assert.IsTrue(File.Exists(TestUtility.TempDir + @"TestTool\JavaApp.class"));

			Directory.Delete(TestUtility.TempDir + @"TestTool", true);
		}


	}
}