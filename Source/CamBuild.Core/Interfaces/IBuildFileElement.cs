using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace CamBuild.Core
{
	public interface IBuildFileElement
	{
		BuildFile ParentBuildFile { get; set; }


	}
}
