using System;
using System.Collections.Generic;
using System.Text;
using CamBuild.Core;
using System.IO;
using CamBuild.Core.Exceptions;
using CamBuild.Core.Attributes;

namespace CamBuild.BasicActions
{
	public class CreateDirectory : IAction
	{
		private string path;
		private BuildFile parentBuildFile;
		private Dictionary<string, string> fields = new Dictionary<string, string>();

		public Dictionary<string, string> Fields
		{
			get { return this.fields; }
		}

		public BuildFile ParentBuildFile
		{
			get { return parentBuildFile; }
			set { parentBuildFile = value; }
		}

		[ActionProperty(true)]
		public string Path
		{
			get { return path; }
			set { path = value; }
		}
		

		public CreateDirectory()
		{
		}


		public void Execute()
		{
			try
			{
				if (!Directory.Exists(this.Path))
					Directory.CreateDirectory(this.Path);
			}
			catch (ArgumentException ex)
			{
				throw new ActionNotExecutedException(this, ex.Message);
			}
		}

		public string Description
		{
			get { return "Creates a directory at the specified path."; }
		}
	}
}
