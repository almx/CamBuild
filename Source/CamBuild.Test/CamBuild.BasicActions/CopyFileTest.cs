using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using NUnit.Framework;
using CamBuild.Core;
using BestBrains.System;
using CamBuild.Core.Exceptions;
using CamBuild.BasicActions;

namespace CamBuild.Test
{
	[TestFixture]
	public class CopyFileTest
	{
		[Test]
		public void TestExecuteMultipleFiles()
		{
			CopyFile cf = new CopyFile();

			TestUtility.CreateFile(TestUtility.TempDir + @"01.testfile");
			TestUtility.CreateFile(TestUtility.TempDir + @"02.testfile");
			Directory.CreateDirectory(TestUtility.TempDir + @"testfiles");

			cf.Source = TestUtility.TempDir + @"*.testfile";
			cf.Destination = TestUtility.TempDir + @"testfiles";
			
			cf.Execute();

			Assert.IsTrue(File.Exists(TestUtility.TempDir + @"testfiles\01.testfile"));
			Assert.IsTrue(File.Exists(TestUtility.TempDir + @"testfiles\02.testfile"));

			// cleanup
			File.Delete(TestUtility.TempDir + @"01.testfile");
			File.Delete(TestUtility.TempDir + @"02.testfile");
			Directory.Delete(TestUtility.TempDir + @"testfiles", true);
		}

		[Test]
		public void TestExecuteSingleFile()
		{
			CopyFile cf = new CopyFile();

			TestUtility.CreateFile(TestUtility.TempDir + @"01.testfile");
			TestUtility.CreateFile(TestUtility.TempDir + @"02.testfile");

			cf.Source = TestUtility.TempDir + @"01.testfile";
			cf.Destination = TestUtility.TempDir + @"02.testfile";

			try
			{
				cf.Execute();
			}
			catch (Exception ex)
			{
				Assert.IsTrue(ex is IOException);
			}

			File.Delete(TestUtility.TempDir + @"02.testfile");

			cf.Execute();

			Assert.IsTrue(File.Exists(TestUtility.TempDir + @"02.testfile"));

			// cleanup
			File.Delete(TestUtility.TempDir + @"01.testfile");
			File.Delete(TestUtility.TempDir + @"02.testfile");
		}


	}
}