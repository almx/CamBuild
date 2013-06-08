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
	public class ExecuteShellCommandTest
	{
		[Test]
		public void ExecuteTest()
		{
			ExecuteShellCommand esc = new ExecuteShellCommand();
			esc.Command = "echo Testing ExecuteTest";

			esc.Execute();

		}
	}
}