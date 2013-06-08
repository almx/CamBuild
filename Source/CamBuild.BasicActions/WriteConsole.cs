using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CamBuild.Core;
using CamBuild.Core.Attributes;

namespace CamBuild.BasicActions
{
	public class WriteConsole : IAction
	{
		private string _message;
		private bool _addNewline = true;
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
		public string Message
		{
			get { return _message; }
			set { _message = value; }
		}

		[ActionProperty(false)]
		public bool AddNewline
		{
			get { return _addNewline; }
			set { _addNewline = value; }
		}
		
		public WriteConsole()
		{

		}
		
		public void Execute()
		{
			if (!_addNewline)
				Console.Write(_message);
			else
				Console.WriteLine(_message);
		}

		public string Description
		{
			get { return "Writes a specified string to the console."; }
		}


	}
}
