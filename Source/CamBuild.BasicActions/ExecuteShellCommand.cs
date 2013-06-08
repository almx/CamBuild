using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using CamBuild.Core;
using CamBuild.Core.Attributes;

namespace CamBuild.BasicActions
{
	public class ExecuteShellCommand : IAction
	{
		private string command;
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
		public string Command
		{
			get { return command; }
			set { command = value; }
		}

		public string Description
		{
			get { return "Executes an arbitrary shell command using cmd.exe."; }
		}

		public ExecuteShellCommand()
		{
		}

		public void Execute()
		{
			ProcessStartInfo psi = new ProcessStartInfo(Environment.GetEnvironmentVariable("COMSPEC"), "/c " + this.command);
			Process esc = Process.Start(psi);
			esc.WaitForExit();

		}


	}
}
