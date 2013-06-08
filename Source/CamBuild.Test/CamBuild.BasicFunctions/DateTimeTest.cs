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
	public class DateTimeTest
	{
		[Test]
		public void Test()
		{
			CamBuild.BasicFunctions.DateTime dt = new CamBuild.BasicFunctions.DateTime();

			List<string> args = new List<string>();
			args.Add("yyyy");

			// pray that the year won't change between call to DateTime.Now and our function evaluation :)
			Assert.AreEqual(System.DateTime.Now.Year.ToString(), dt.GetResult(args));

		}
	}
}