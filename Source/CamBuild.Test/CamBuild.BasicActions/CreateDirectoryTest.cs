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
	public class CreateDirectoryTest
	{
		[Test]
		public void ExecuteTest()
		{
			string path = TestUtility.TempDir + @"tempdir" + new Random().Next(10000000);

			if(Directory.Exists(path))
				Directory.Delete(path, true);

			CreateDirectory cd = new CreateDirectory();
			cd.Path = path;

			cd.Execute();

			Assert.IsTrue(Directory.Exists(path));

			//cleanup
			Directory.Delete(path);
		}
	}
}