using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CamBuild.Core.Exceptions;

namespace CamBuild.Core
{
	public interface IAction : IBuildFileElement
	{
		Dictionary<string, string> Fields { get; }

		void Execute();
		string Description { get; }
	}
}
