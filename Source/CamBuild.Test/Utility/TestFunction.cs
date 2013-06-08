using System;
using System.Collections.Generic;
using System.Text;
using CamBuild.Core;

namespace CamBuild.Test
{
	public class TestFunction : IFunction
	{
		public string GetResult(ICollection<string> args)
		{
			return "!testvalue!";
		}

		public string Description
		{
			get { return "Test function."; }
		}
	}
}
