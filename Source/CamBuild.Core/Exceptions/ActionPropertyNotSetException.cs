using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace CamBuild.Core.Exceptions
{
	[Serializable]
	public class ActionPropertyNotSetException : Exception
	{
		private object _obj;
		private string _message;
		private PropertyInfo _propertyInfo;

		public PropertyInfo PropertyInfo
		{
			get { return _propertyInfo; }
			set { _propertyInfo = value; }
		}

		public override string Message
		{
			get
			{
				return _message;
			}
		}

		public object Object
		{
			get { return _obj; }
			set { _obj = value; }
		}

		public ActionPropertyNotSetException(string message)
		{
			_message = message;
		}

		public ActionPropertyNotSetException(object obj, PropertyInfo propertyInfo, string message)
		{
			_obj = obj;
			_propertyInfo = propertyInfo;
			_message = message;

		}
	}
}
