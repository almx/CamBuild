using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using NUnit.Framework;
using CamBuild.Core;
using BestBrains.System;
using CamBuild.BasicFunctions;

namespace CamBuild.Test
{
	[TestFixture]
	public class IncrementedBuildNumberTest
	{
		[Test]
		public void TestGetResult()
		{
			string testDir = TestUtility.TempDir + @"IncrementedBuildNumberTest\";

			if (Directory.Exists(testDir))
				Directory.Delete(testDir, true);

			Directory.CreateDirectory(testDir);
			Directory.CreateDirectory(testDir + "1.1.0");
			Directory.CreateDirectory(testDir + "1.1.2");
			Directory.CreateDirectory(testDir + "1.1.10");
			Directory.CreateDirectory(testDir + "1.1.15");
			
			IncrementedBuildNumber ibn = new IncrementedBuildNumber();
			
			List<string> args = new List<string>();
			args.Add(TestUtility.TempDir + @"IncrementedBuildNumberTest");
			args.Add(@"1.1.*");

			string newDir = ibn.GetResult(args);

			Assert.AreEqual(testDir + "1.1.16", newDir);

			Directory.Delete(testDir, true);

			// test without existing dirs present
			ibn = new IncrementedBuildNumber();
			
			args = new List<string>();
			args.Add(TestUtility.TempDir + @"IncrementedBuildNumberTest");
			args.Add(@"*.*.*");

			newDir = ibn.GetResult(args);

			Assert.AreEqual(testDir + "1.0.0", newDir);
		}
	}
}