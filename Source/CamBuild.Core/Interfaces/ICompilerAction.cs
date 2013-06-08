using System;
using System.Collections.Generic;
using System.Text;
using CamBuild.Core.Attributes;

namespace CamBuild.Core
{
	public interface ICompilerAction
	{
		string SourceFiles { get; set; }
		string CommandString { get; set; }

		bool ForceRebuild { get; set; }
	}
}
