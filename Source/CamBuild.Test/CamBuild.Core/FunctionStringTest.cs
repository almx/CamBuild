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
	public class FunctionStringTest
	{
		[Test]
		public void Test()
		{
			string str = "MyFunction(, , arg1,arg2 , [$Function()] ,, ,)";

			FunctionString func = new FunctionString(str);

			Assert.AreEqual("MyFunction", func.Name);
			Assert.AreEqual(3, func.Args.Count);
			Assert.AreEqual("arg1", ((List<string>)func.Args)[0]);
			Assert.AreEqual("arg2", ((List<string>)func.Args)[1]);
			Assert.AreEqual("[$Function()]", ((List<string>)func.Args)[2]);
		}

	}
}