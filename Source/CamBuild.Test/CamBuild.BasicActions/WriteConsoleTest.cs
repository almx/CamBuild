using System;
using System.Collections.Generic;
using System.Text;
using CamBuild.Core;
using CamBuild.BasicActions;
using System.IO;
using NUnit.Framework;

namespace CamBuild.Test
{
	[TestFixture]
	public class WriteConsoleTest
	{
		[Test]
		public void ExecuteTest()
		{
			string expectedMessage = "#Test Message123";

			WriteConsole wc = new WriteConsole();
			wc.Message = expectedMessage;
			wc.AddNewline = true;

			StringWriter sw = new StringWriter();
			System.Console.SetOut(sw);

			wc.Execute();

			Assert.AreEqual(expectedMessage + Environment.NewLine, sw.ToString());


			// cleanup
			StreamWriter standardOutput = new StreamWriter(System.Console.OpenStandardOutput());
			standardOutput.AutoFlush = true;

			System.Console.SetOut(standardOutput);

		}
		
		
		
		
	}
}
