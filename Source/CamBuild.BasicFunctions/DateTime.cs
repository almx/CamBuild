using System;
using System.Collections.Generic;
using System.Text;
using CamBuild.Core;

namespace CamBuild.BasicFunctions
{
	public class DateTime : IFunction
	{
		// args[0]: date/time format string to be passed to DateTime.ToString()
		public string GetResult(ICollection<string> args)
		{
			return System.DateTime.Now.ToString(((List<string>)args)[0]);	// "yyyy-MM-dd-hh:mm:ss" for example
		}

		public string Description
		{
			get { return "Returns the date and/or time"; }
		}


	}
}
