using System;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace CamBuild.Console
{
	// Class taken from http://www.thecodeproject.com/csharp/command_line.asp
	// Namespace and class name changed, variable names changed
	// Property 'ParameterCount' added
	public class CommandLineArguments
	{
		// Variables
		private StringDictionary parameters;

		public int ParameterCount
		{
			get { return this.parameters.Count; }
		}

		// Retrieve a parameter value if it exists
		public string this[string param]
		{
		    get
		    {
		        return (this.parameters[param]);
		    }
		}

		// Constructor
		public CommandLineArguments(string[] Args)
		{
			parameters = new StringDictionary();
			Regex splitter = new Regex(@"^-{1,2}|^/|=|:", RegexOptions.IgnoreCase | RegexOptions.Compiled);
			Regex remover = new Regex(@"^['""]?(.*?)['""]?$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
			string parameter = null;
			string[] parts;

			// Valid parameters forms:
			// {-,/,--}param{ ,=,:}((",')value(",'))
			// Examples: -param1 value1 --param2 /param3:"Test-:-work" /param4=happy -param5 '--=nice=--'
			foreach (string txt in Args)
			{
				// Look for new parameters (-,/ or --) and a possible enclosed value (=,:)
				parts = splitter.Split(txt, 3);
				switch (parts.Length)
				{
					// Found a value (for the last parameter found (space separator))
					case 1:
						if (parameter != null)
						{
							if (!parameters.ContainsKey(parameter))
							{
								parts[0] = remover.Replace(parts[0], "$1");
								parameters.Add(parameter, parts[0]);
							}
							parameter = null;
						}
						// else Error: no parameter waiting for a value (skipped)
						break;
					// Found just a parameter
					case 2:
						// The last parameter is still waiting. With no value, set it to true.
						if (parameter != null)
						{
							if (!parameters.ContainsKey(parameter))
								parameters.Add(parameter, "true");
						}
						parameter = parts[1];
						break;
					// Parameter with enclosed value
					case 3:
						// The last parameter is still waiting. With no value, set it to true.
						if (parameter != null)
						{
							if (!parameters.ContainsKey(parameter))
								parameters.Add(parameter, "true");
						}
						parameter = parts[1];
						// Remove possible enclosing characters (",')
						if (!parameters.ContainsKey(parameter))
						{
							parts[2] = remover.Replace(parts[2], "$1");
							parameters.Add(parameter, parts[2]);
						}
						parameter = null;
						break;
				}
			}
			// In case a parameter is still waiting
			if (parameter != null)
			{
				if (!parameters.ContainsKey(parameter)) parameters.Add(parameter, "true");
			}
		}


	}
}