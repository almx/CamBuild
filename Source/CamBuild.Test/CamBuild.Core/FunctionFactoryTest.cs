using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using NUnit.Framework;
using CamBuild.Core;
using BestBrains.System;
using CamBuild.Core.Exceptions;

namespace CamBuild.Test
{
	[TestFixture]
	public class FunctionFactoryTest
	{
		[Test]
		public void CreateTest()
		{
			IFunction bf = FunctionFactory.Create("DateTime");

			Assert.IsTrue(bf.GetType().Name == "DateTime");

			try
			{
				FunctionFactory.Create("jasd98asjd98ajd98a");
			}
			catch (Exception ex)
			{
				Assert.IsTrue(ex is UnrecognizedFunctionException);
			}



		}
	}
}