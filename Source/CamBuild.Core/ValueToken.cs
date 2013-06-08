using System;
using System.Collections.Generic;
using System.Text;

namespace CamBuild.Core
{
	public enum ValueTokenType
	{
		Function,
		Property,
		Text
	}

	public class ValueToken
	{
		private ValueTokenType type;
		private string text;

		public string Text
		{
			get { return text; }
			set { text = value; }
		}

		public ValueTokenType Type
		{
			get { return type; }
			set { type = value; }
		}

		public ValueToken(ValueTokenType type, string text)
		{
			this.type = type;
			this.text = text;
		}
		
	}
}
