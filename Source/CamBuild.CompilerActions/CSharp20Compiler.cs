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
	public class CSharp20Compiler : IAction, ICompilerAction
	{
		private BuildFile parentBuildFile;
		private Dictionary<string, string> fields = new Dictionary<string, string>();
		private string sourceFiles = "";
		private string commandString = "";
		private bool forceRebuild;
		private string compilerExecutable = Environment.GetEnvironmentVariable("WINDIR") + @"\Microsoft.NET\Framework\v2.0.50727\csc.exe";
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

		[ActionProperty(false)]
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

		public CSharp20Compiler()
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
					case "delaysign":
					case "optimize":
					case "warnaserror":
					case "checked":
					case "unsafe":
					case "help":
					case "nologo":
					case "noconfig":
					case "utf8output":
					case "fullpaths":
					case "nostdlib":
						this.AddArg("/", name, "", val);
						break;

					case "out":
					case "target":
					case "doc":
					case "keyfile":
					case "keycontainer":
					case "platform":
					case "recurse":
					case "reference":
					case "addmodule":
					case "win32res":
					case "win32icon":
					case "resource":
					case "linkresource":
					case "warn":
					case "nowarn":
					case "define":
					case "langversion":
					case "baseaddress":
					case "bugreport":
					case "codepage":
					case "main":
					case "filealign":
					case "pdb":
					case "lib":
					case "errorreport":
					case "moduleassemblyname":
						this.AddArg("/", name, ":", val);
						break;

					case "debug":
						{
							if (val == "" || val == "+" || val == "-")
								this.AddArg("/", name, "", val);
							else
								this.AddArg("/", name, ":", val);
							break;
						}
				}

			}

			if (this.CommandString.Length > 0)
				this.args.Add(this.CommandString);

			if (this.SourceFiles.Length > 0)
				this.args.Add(this.SourceFiles);
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

		private List<string> GetSourceFiles()
		{
			List<string> files = new List<string>();

			if (this.Fields["recurse"] != null)
				files.AddRange(Utility.GetFileListFromWildcardString(this.Fields["recurse"].Trim(new char[] { '\"' })));

			files.AddRange(Utility.TokenizeSourceFilesString(this.SourceFiles));

			return files;			
		}

		private bool ShouldRebuild()
		{
			if (this.ForceRebuild)
				return true;

			string outFile = this.Fields["out"].Trim(new char[] { '\"' });

			if (!File.Exists(outFile))
				return true; 
			
			foreach (string file in this.GetSourceFiles())
			{
				if (File.GetLastWriteTime(file).CompareTo(File.GetLastWriteTime(outFile)) > 0)
					return true;
			}

			List<string> refFiles = new List<string>(); ;

			if (this.Fields.ContainsKey("reference"))
			{
				refFiles = (List<string>)Utility.TokenizeSourceFilesString(this.Fields["reference"]);

				foreach (string refFile in refFiles)
				{
					DateTime lastModRefFile = File.GetLastWriteTime(this.Fields["out"].Trim(new char[] { '\"' }));

					if (File.GetLastWriteTime(refFile).CompareTo(lastModRefFile) > 0)
						return true;
				}
			}

			return false;
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
			if (!this.Fields.ContainsKey("out"))
				throw new ActionNotExecutedException(this, "Required parameter /out was not supplied");

			if (!this.Fields.ContainsKey("target"))
				throw new ActionNotExecutedException(this, "Required parameter /target was not supplied");
		}

		public string Description
		{
			get { return "Calls csc.exe in order to compile C# v2.0.50727 code."; }
		}

	}
}
