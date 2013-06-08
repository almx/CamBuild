using System;
using System.Collections.Generic;
using System.Text;

namespace CamBuild.Core
{
	public interface IFunction
	{
		string Description { get; }
		string GetResult(ICollection<string> args);
	}
}
