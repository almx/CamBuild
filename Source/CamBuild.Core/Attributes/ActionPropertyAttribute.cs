using System;
using System.Collections.Generic;
using System.Text;

namespace CamBuild.Core.Attributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class ActionPropertyAttribute : Attribute
	{
		private bool _isRequired;
		
		public bool IsRequired
		{
			get { return _isRequired; }
		}

		public ActionPropertyAttribute(bool isRequired)
		{
			_isRequired = isRequired;
		}

	}
}
