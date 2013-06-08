using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using NUnit.Framework;
using CamBuild.Core;
using BestBrains.System;
using System.Diagnostics;
using System.Threading;
using CamBuild.Core.Exceptions;

namespace CamBuild.Test
{
	[TestFixture]
	public class CSharp20CompilerTest
	{
		[Test]
		public void GetInstanceTest()
		{
			XmlDocument xd = new XmlDocument();
			xd.LoadXml(this.GetSample());

			IAction csc = (IAction)BuildFileElementFactory.Create((XmlElement)xd.SelectSingleNode("//CSharp20Compiler"), null);

			Assert.AreEqual("\"C:\\Temp\\TestLibrary.dll\"", csc.Fields["out"]);
		}

		[Test]
		public void ExecuteTest()
		{
			XmlDocument xd = new XmlDocument();
			xd.LoadXml(this.GetSample());

			IAction csc = (IAction)BuildFileElementFactory.Create((XmlElement)xd.SelectSingleNode("//CSharp20Compiler"), null);

			File.Delete(TestUtility.TempDir + @"TestLibrary.dll");

			csc.Execute();

			Assert.IsTrue(File.Exists(TestUtility.TempDir + @"TestLibrary.dll"));
			File.Delete(TestUtility.TempDir + @"TestLibrary.dll");
		}


		[Test]
		public void ExecuteTestSourceFilesArg()
		{
			XmlDocument xd = new XmlDocument();
			xd.LoadXml(this.GetSample());

			IAction csc = (IAction)BuildFileElementFactory.Create((XmlElement)xd.SelectSingleNode("//CSharp20Compiler"), null);

			File.Delete(TestUtility.TempDir + @"TestLibrary.dll");

			csc.Execute();

			Assert.IsTrue(File.Exists(TestUtility.TempDir + @"TestLibrary.dll"));
			File.Delete(TestUtility.TempDir + @"TestLibrary.dll");
		}

		[Test]
		public void ExecuteTestShouldRebuildWithReference()
		{
			File.Delete(TestUtility.TempDir + @"TestLibrary.dll");
			File.Delete(TestUtility.TempDir + @"TestApplication.exe");

			XmlDocument xdLib = new XmlDocument();
			xdLib.LoadXml(this.GetSample());
			XmlDocument xdExe = new XmlDocument();
			xdExe.LoadXml(this.GetSampleReference());

			// build TestLibrary.dll
			IAction cscLib = (IAction)BuildFileElementFactory.Create((XmlElement)xdLib.SelectSingleNode("//CSharp20Compiler"), null);
			ActionPropertySetter.SetProperties(cscLib);

			cscLib.Execute();
			Assert.IsTrue(File.Exists(TestUtility.TempDir + @"TestLibrary.dll"));

			// build TestAplication.exe
			IAction cscExe = (IAction)BuildFileElementFactory.Create((XmlElement)xdExe.SelectSingleNode("//CSharp20Compiler"), null);
			ActionPropertySetter.SetProperties(cscExe);

			cscExe.Execute();
			Assert.IsTrue(File.Exists(TestUtility.TempDir + @"TestApplication.exe"));

			// attempt to build TestApplication.exe again, verify that it is not rebuilt
			DateTime exeMod = File.GetLastWriteTime(TestUtility.TempDir + @"TestApplication.exe");

			IAction cscExe2 = (IAction)BuildFileElementFactory.Create((XmlElement)xdExe.SelectSingleNode("//CSharp20Compiler"), null);
			ActionPropertySetter.SetProperties(cscExe2);
			cscExe2.Execute();

			Assert.AreEqual(exeMod, File.GetLastWriteTime(TestUtility.TempDir + @"TestApplication.exe"));

			// delete TestLibrary, rebuild it
			File.Delete(TestUtility.TempDir + @"TestLibrary.dll");

			IAction cscLib2 = (IAction)BuildFileElementFactory.Create((XmlElement)xdLib.SelectSingleNode("//CSharp20Compiler"), null);
			ActionPropertySetter.SetProperties(cscLib2);
			cscLib2.Execute();

			// attempt to build TestApplication.exe again, verify that it is actually rebuilt
			IAction cscExe3 = (IAction)BuildFileElementFactory.Create((XmlElement)xdExe.SelectSingleNode("//CSharp20Compiler"), null);
			ActionPropertySetter.SetProperties(cscExe3);
			cscExe3.Execute();

			Assert.IsTrue(exeMod.CompareTo(File.GetLastWriteTime(TestUtility.TempDir + @"TestApplication.exe")) < 0);

			File.Delete(TestUtility.TempDir + @"TestLibrary.dll");
			File.Delete(TestUtility.TempDir + @"TestApplication.exe");
		}

		///<sample>
		///    <CSharp20Compiler>
		///        <target>library</target>
		///    </CSharp20Compiler>
		///</sample>
		[Test]
		public void TestIncompleteArgsExecute()
		{
			XmlDocument xd = new XmlDocument();
			xd.LoadXml(CommentReader.GetElement("sample"));

			IAction csc = (IAction)BuildFileElementFactory.Create((XmlElement)xd.SelectSingleNode("//CSharp20Compiler"), null);
			ActionPropertySetter.SetProperties(csc);

			try
			{
				csc.Execute();
			}
			catch (Exception ex)
			{
				Assert.IsTrue(ex is ActionNotExecutedException);
			}
		}

		///<sample>
		///    <CSharp20Compiler>
		///        <out>"C:\Temp\TestLibrary.dll"</out>
		///        <target>library</target>
		///		   <recurse>"E:\Alexander\Projects\CamBuild\Source\TestProjects\TestTool\Source\TestLibrary\Properties\*.cs"</recurse>
		///        <SourceFiles>"E:\Alexander\Projects\CamBuild\Source\TestProjects\TestTool\Source\TestLibrary\TestClass.cs"</SourceFiles>
		///    </CSharp20Compiler>
		///</sample>
		private string GetSample()
		{
			return CommentReader.GetElement("sample");

		}

		///<sample>
		///    <CSharp20Compiler>
		///        <out>"C:\Temp\TestApplication.exe"</out>
		///        <target>exe</target>
		///		   <recurse>"E:\Alexander\Projects\CamBuild\Source\TestProjects\TestTool\Source\TestApplication\*.cs"</recurse>
		///		   <reference>"C:\Temp\TestLibrary.dll"</reference>
		///    </CSharp20Compiler>
		///</sample>
		private string GetSampleReference()
		{
			return CommentReader.GetElement("sample");

		}
	}
}