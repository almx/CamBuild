using System;
using System.Collections.Generic;
using System.Text;
using CamBuild.Core;
using CamBuild.Core.Exceptions;
using System.IO;
using CamBuild.Core.Attributes;

namespace CamBuild.BasicActions
{
	public class DeleteFile : IAction
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

		public string Description
		{
			get { return "Deletes a specified file."; }
		}


		public DeleteFile()
		{
		}

		public void Execute()
		{
			File.Delete(path);

			if (File.Exists(path))
				throw new ActionNotExecutedException(this, "File was not deleted");
		}
	}
}
