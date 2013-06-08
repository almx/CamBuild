using System;
using System.Collections.Generic;
using System.Text;

namespace CamBuild.Core
{
	public interface ILogger
	{
		void Write(string type, string text);
	}
}
