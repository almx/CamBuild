using System;
using System.Collections.Generic;
using System.Text;
using CamBuild.Core;
using CamBuild.Core.Exceptions;
using System.IO;
using CamBuild.Core.Attributes;


namespace CamBuild.BasicActions
{
	public class CopyFile : IAction
	{
		private string source;
		private string destination;
		private bool overWrite = false;
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
		public string Destination
		{
			get { return destination; }
			set { destination = value; }
		}

		[ActionProperty(true)]
		public string Source
		{
			get { return source; }
			set { source = value; }
		}

		[ActionProperty(false)]
		public bool OverWrite
		{
			get { return overWrite; }
			set { overWrite = value; }
		}


		public CopyFile()
		{
		}


		public void Execute()
		{
			// if source is a wildcard, destination must be a dir
			// if source is a single file, destination can be a dir or file
			string[] files = Directory.GetFiles(Path.GetDirectoryName(source), Path.GetFileName(source), SearchOption.TopDirectoryOnly);

			if (files.Length == 0)
				return;

			if (files.Length == 1)
			{
				File.Copy(files[0], this.destination, this.overWrite);
			}
			else
			{
				if (!Directory.Exists(destination))
					throw new ActionNotExecutedException(this, "Destination is not an existing directory, attempt to copy multiple files aborted");

				foreach (string file in files)
				{
					File.Copy(file, this.destination + Path.DirectorySeparatorChar + Path.GetFileName(file), this.overWrite);
				}
			}
			


		}

		public string Description
		{
			get { return "Copies file(s), wildcards allowed in source."; }
		}
	}
}
