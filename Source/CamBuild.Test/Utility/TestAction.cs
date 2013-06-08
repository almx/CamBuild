using System;
using System.Collections.Generic;
using System.Text;
using CamBuild.Core.Attributes;
using CamBuild.Core.Exceptions;
using CamBuild.Core;

namespace CamBuild.Test
{
	public class TestAction : IAction
	{
		private string _message;
		private int _var;
		private string _var2 = "";
		private BuildFile parentBuildFile;
		private Dictionary<string, string> fields = new Dictionary<string,string>();

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
		public string Var2
		{
			get { return _var2; }
			set { _var2 = value; }
		}

		[ActionProperty(false)]
		public int Var
		{
			get { return _var; }
			set { _var = value; }
		}

		[ActionProperty(true)]
		public string Message
		{
			get { return _message; }
			set { _message = value; }
		}

		public void Execute()
		{
			
		}

		public string Description
		{
			get { return "Test action."; }
		}

	}
}
