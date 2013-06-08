using System;
using System.Collections.Generic;
using System.Text;

namespace CamBuild.Core.Exceptions
{
	[Serializable]
	public class ActionNotExecutedException : Exception
	{
		private IAction _action;
		private string _message;

		public override string Message
		{
			get	{ return _message; }
		}

		public IAction Action
		{
			get { return _action; }
			set { _action = value; }
		}

		public ActionNotExecutedException(string message)
		{
			_message = message;
		}
		
		public ActionNotExecutedException(IAction action, string message)
		{
			_action = action;
			_message = message;
		}
		
	}
}
