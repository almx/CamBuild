using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using NUnit.Framework;
using CamBuild.Core;
using BestBrains.System;
using CamBuild.BasicActions;

namespace CamBuild.Test
{
	[TestFixture]
	public class DeleteFileTest
	{
		[Test]
		public void TestExecute()
		{
			string tempFile = TestUtility.TempDir + @"testfile" + new Random().Next(100000000);

			File.Delete(tempFile);

			DeleteFile df = new DeleteFile();
			df.Path = tempFile;

			TestUtility.CreateFile(tempFile);

			df.Execute();

			Assert.IsFalse(File.Exists(tempFile));
			

			//cleanup
			File.Delete(tempFile);
		}
	}
}