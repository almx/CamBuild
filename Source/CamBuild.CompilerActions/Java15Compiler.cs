using System;
using System.Collections.Generic;
using System.Text;
using CamBuild.Core;
using CamBuild.Core.Attributes;
using System.Xml;
using System.Diagnostics;
using System.IO;
using CamBuild.Core.Exceptions;

namespace CamBuild.CompilerActions
{
	public class Java15Compiler : IAction, ICompilerAction
	{
		private BuildFile parentBuildFile;
		private Dictionary<string, string> fields = new Dictionary<string, string>();
		private string sourceFiles = "";
		private string commandString = "";
		private bool forceRebuild;
		private string compilerExecutable = Environment.GetEnvironmentVariable("PROGRAMFILES") + @"\Java\jdk1.5.0_08\bin\javac.exe";
		private List<string> args = new List<string>();

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
		public string SourceFiles
		{
			get { return this.sourceFiles; }
			set { this.sourceFiles = value; }
		}

		[ActionProperty(false)]
		public string CommandString
		{
			get { return commandString; }
			set { commandString = value; }
		}

		public bool ForceRebuild
		{
			get { return forceRebuild; }
			set { forceRebuild = value; }
		}


		public Java15Compiler()
		{
		}

		private void ParseFields()
		{
			foreach (string key in this.Fields.Keys)
			{
				string name = key;
				string val = this.Fields[key];

				switch (key)
				{
					case "g":
						{
							if (val == "")
								this.AddArg("-", name, "", "");
							else
								this.AddArg("-", name, ":", val);

							break;
						}
					case "nowarn":
					case "verbose":
					case "deprecation":
					case "version":
					case "help":
					case "X":
						this.AddArg("-", name, "", "");
						break;

					case "classpath":
					case "cp":
					case "sourcepath":
					case "bootclasspath":
					case "extdirs":
					case "endorseddirs":
					case "d":
					case "encoding":
					case "source":
					case "target":
						this.AddArg("-", name, " ", val);
						break;

				}

			}

			if (this.CommandString.Length > 0)
				this.args.Add(this.CommandString);

			if (this.SourceFiles.Length > 0)
				this.args.Add(this.SourceFiles);
		}

		private bool ShouldRebuild()
		{
			if (this.ForceRebuild)
				return true;

			foreach (string file in Utility.TokenizeSourceFilesString(this.SourceFiles))
			{
				string outFile = "";

				if (this.Fields["d"] != null)
					outFile += this.Fields["d"].Trim(new char[] { '\"' }) + @"\";	// remove quotes and add trailing '\' to be safe

				string fileName = Path.GetFileNameWithoutExtension(file);
				outFile += fileName + ".class";

				if (!File.Exists(outFile))
					return true;

				if (File.GetLastWriteTime(file.Trim(new char[]{ '\"' })).CompareTo(File.GetLastWriteTime(outFile)) > 0)
					return true;
			}

			return false;
		}

		private string FullArgString
		{
			get
			{
				string strArgs = "";

				foreach (string arg in this.args)
				{
					strArgs += arg + " ";
				}

				return strArgs.TrimEnd(new char[] { ' ' });
			}
		}

		private void AddArg(string iniChar, string name, string separator, string value)
		{
			this.args.Add(iniChar + name + separator + value);
		}

		public void Execute()
		{
			this.ParseFields();
			this.ValidateParameters();

			if (this.ShouldRebuild())
			{
				ProcessStartInfo psi = new ProcessStartInfo(this.compilerExecutable, this.FullArgString);
				Process csc = Process.Start(psi);
				csc.WaitForExit();
			}
		}

		private void ValidateParameters()
		{
			if (Utility.TokenizeSourceFilesString(this.SourceFiles).Count == 0)
				throw new ActionNotExecutedException(this, "No source file(s) were specified");
		}

		public string Description
		{
			get { return "Calls javac.exe in order to compile Java v1.5.0_08 code."; }
		}

	}
}
