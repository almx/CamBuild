using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using NUnit.Framework;
using CamBuild.Core;
using BestBrains.System;
using System.Diagnostics;
using CamBuild.Console;

namespace CamBuild.Test
{
	[TestFixture]
	public class CamBuildConsoleTest
	{
		[Test]
		public void MainTest()
		{
			string curDir = Directory.GetCurrentDirectory();

			Directory.SetCurrentDirectory(TestUtility.TempDir);

			if (Directory.Exists(TestUtility.TempDir + @"NewDir"))
				Directory.Delete(TestUtility.TempDir + @"NewDir", true);

			List<string> al = new List<string>();
			al.Add(@"-bf:TestTool.xml");
			al.Add(@"-bc:JavaApp;TestLibrary");
			al.Add(@"-prop:OutputDir=C:\Temp\NewDir");
			al.Add(@"-log:xml");

			string[] args = new string[al.Count];
			al.CopyTo(args, 0);
			
			Program.Main(args);

			Assert.IsTrue(Directory.Exists(TestUtility.TempDir + @"NewDir"));
			Assert.IsTrue(File.Exists(TestUtility.TempDir + @"NewDir\JavaApp.class"));
			Assert.IsTrue(File.Exists(TestUtility.TempDir + @"NewDir\TestLibrary.dll"));
			Assert.IsTrue(File.Exists(TestUtility.TempDir + @"TestTool.log.xml"));

			DateTime dateModTestLibrary = File.GetLastWriteTime(TestUtility.TempDir + @"NewDir\TestLibrary.dll");
			DateTime dateModJavaApp = File.GetLastWriteTime(TestUtility.TempDir + @"NewDir\JavaApp.class");

			al.Add(@"-rebuild:JavaApp");
			args = new string[al.Count];
			al.CopyTo(args, 0);

			Program.Main(args);

			Assert.AreEqual(dateModTestLibrary, File.GetLastWriteTime(TestUtility.TempDir + @"NewDir\TestLibrary.dll"));
			Assert.AreNotEqual(dateModJavaApp, File.GetLastWriteTime(TestUtility.TempDir + @"NewDir\JavaApp.class"));

			// cleanup
			if (Directory.Exists(TestUtility.TempDir + @"NewDir"))
				Directory.Delete(TestUtility.TempDir + @"NewDir", true);

			File.Delete(TestUtility.TempDir + @"TestTool.log.xml");

			Directory.SetCurrentDirectory(curDir);
		}
	}
}