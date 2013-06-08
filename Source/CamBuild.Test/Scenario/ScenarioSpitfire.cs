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
	public class ScenarioSpitfire
	{
		[Ignore]
		public void Test()
		{
			BuildFile bf = new BuildFile();
			bf.LoadXmlFile(@"..\..\Scenario\Spitfire.xml");

			if (Directory.Exists(TestUtility.TempDir + @"Spitfire"))
				Directory.Delete(TestUtility.TempDir + @"Spitfire", true);

			List<IBuildFileElement> elements = (List<IBuildFileElement>)bf.RootElements;
			new BuildFileElementExecutor().ExecuteElements(elements);

			Assert.IsTrue(File.Exists(TestUtility.TempDir + @"Spitfire\Storage.dll"));
			Assert.IsTrue(File.Exists(TestUtility.TempDir + @"Spitfire\CommandCore.dll"));
			Assert.IsTrue(File.Exists(TestUtility.TempDir + @"Spitfire\NetworkModel.dll"));
			Assert.IsTrue(File.Exists(TestUtility.TempDir + @"Spitfire\ClientCache.dll"));
			Assert.IsTrue(File.Exists(TestUtility.TempDir + @"Spitfire\MSSQLProvider.dll"));
			Assert.IsTrue(File.Exists(TestUtility.TempDir + @"Spitfire\Server.exe"));
			Assert.IsTrue(File.Exists(TestUtility.TempDir + @"Spitfire\ScmClient.exe"));


			Directory.Delete(TestUtility.TempDir + @"Spitfire", true);
		}
	}
}