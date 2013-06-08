using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using CamBuild.Core.Exceptions;
using System.Text.RegularExpressions;

namespace CamBuild.Core
{
	public class FunctionString
	{
		private string functionString;
		private static Regex regArgs = new Regex(@".+?\((?<args>.+)\)", RegexOptions.Compiled);
		private static Regex regName = new Regex(@"(?<name>.+?)\(.*\)", RegexOptions.Compiled);
		private static Regex regValidate = new Regex(@".+\(.*\)", RegexOptions.Compiled);

		public ICollection<string> Args
		{
			get
			{
				Match match = FunctionString.regArgs.Match(this.functionString);

				string[] argTokens = match.Groups["args"].Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

				List<string> args = new List<string>();

				foreach (string token in argTokens)
				{
					if(token.Trim().Length > 0)
						args.Add(token.Trim());
				}

				return args;
			}
		}

		public string Name
		{
			get
			{
				Match match = regName.Match(this.functionString);

				return match.Groups["name"].Value;
			}
		}

		public FunctionString(string functionString)
		{
			this.functionString = functionString;

			this.Validate();
		}

		private void Validate()
		{
			Match match = regValidate.Match(this.functionString);

			if(!match.Success)
				throw new FunctionStringParseException("Function string contains invalid syntax.", this.functionString);
		}

	}
}
