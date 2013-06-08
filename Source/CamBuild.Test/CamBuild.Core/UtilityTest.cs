using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using NUnit.Framework;
using CamBuild.Core;

namespace CamBuild.Test
{
	[TestFixture]
	public class UtilityTest
	{
		[Test]
		public void GetExecutablePathTest()
		{
#if DEBUG
			Assert.IsTrue(Utility.ExecutablePath.Contains("CamBuild.Core.dll"));
#endif
		}

		[Test]
		public void GetTypesFromAssemblyFilesTest()
		{
			List<Type> typesActual = (List<Type>)Utility.GetTypesFromAssemblyFiles(Path.GetDirectoryName(Utility.ExecutablePath), "IAction");

			foreach (Type type in typesActual)
			{
				Assert.IsTrue(type.GetInterface("IAction") == typeof(IAction));
			}

		}


		[Test]
		public void TokenizeTest()
		{
			string sourceFiles = "\"C:\\Temp\\file1\" \"file2\" \"file3\"  ";

			List<string> files = (List<string>)Utility.TokenizeSourceFilesString(sourceFiles);

			Assert.AreEqual(@"C:\Temp\file1", files[0]);
			Assert.AreEqual("file2", files[1]);
			Assert.AreEqual("file3", files[2]);

		}

		[Test]
		public void GetFileListFromWildcardStringTest()
		{
			File.Delete(TestUtility.TempDir + @"file1.cs");
			File.Delete(TestUtility.TempDir + @"file2.test.cs");

			TestUtility.CreateFile(TestUtility.TempDir + @"file1.cs");
			TestUtility.CreateFile(TestUtility.TempDir + @"file2.test.cs");

			string str = TestUtility.TempDir + @"*.cs";

			List<string> files = (List<string>)Utility.GetFileListFromWildcardString(str);

			Assert.AreEqual(2, files.Count);
			Assert.IsTrue(files[0].Contains("file1.cs") || files[1].Contains("file2.test.cs"));

			File.Delete(TestUtility.TempDir + @"file1.cs");
			File.Delete(TestUtility.TempDir + @"file2.test.cs");
		}
	}
}