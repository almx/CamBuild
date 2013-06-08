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
	public class Java15CompilerTest
	{
		[Test]
		public void ExecuteTest()
		{
			XmlDocument xd = new XmlDocument();
			xd.LoadXml(this.GetSample());

			IAction jc = (IAction)BuildFileElementFactory.Create((XmlElement)xd.SelectSingleNode("//Java15Compiler"), null);
			ActionPropertySetter.SetProperties(jc);

			File.Delete(TestUtility.TempDir + @"JavaApp.class");

			jc.Execute();

			Assert.IsTrue(File.Exists(TestUtility.TempDir + @"JavaApp.class"));

			// cleanup
			File.Delete(TestUtility.TempDir + @"JavaApp.class");
		}

		///<sample>
		///    <Java15Compiler>
		///		   <g>none</g> 
		///        <verbose></verbose>
		///        <d>"c:\temp"</d>
		///		   <SourceFiles>"E:\Alexander\Projects\CamBuild\Source\TestProjects\TestTool\Source\JavaApp\JavaApp.java"</SourceFiles>
		///        <AdditionalArgs></AdditionalArgs>
		///    </Java15Compiler>
		///</sample>
		private string GetSample()
		{
			return CommentReader.GetElement("sample");
		}
	}
}