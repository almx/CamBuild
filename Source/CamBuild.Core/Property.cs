using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace CamBuild.Core
{
	public class Property : IBuildFileElement
	{
		private string value;
		private string name;
		private BuildFile parentBuildFile;

		public BuildFile ParentBuildFile
		{
			get { return parentBuildFile; }
			set { parentBuildFile = value; }
		}


		public string Name
		{
			get { return this.name; }
		}

		public string Value
		{
			get { return this.value; }
			set { this.value = value; }
		}

		public Property(string name, string value)
		{
			this.name = name;
			this.value = value;
		}

	}
}
