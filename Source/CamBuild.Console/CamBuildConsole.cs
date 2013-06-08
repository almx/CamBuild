using System;
using System.Collections.Generic;
using System.Text;
using CamBuild.Core;
using CamBuild.Console.Exceptions;
using System.IO;

namespace CamBuild.Console
{
	public class CamBuildConsole
	{
		private CommandLineArguments cla;

		public CamBuildConsole(CommandLineArguments cla)
		{
			this.cla = cla;

			System.Console.WriteLine("------------------------------------------------------------");
			System.Console.WriteLine("| CamBuild Console Application");
			System.Console.WriteLine("------------------------------------------------------------");
			System.Console.WriteLine();

			this.HandleArguments();
		}

		private void HandleArguments()
		{
			if (cla.ParameterCount == 0 || cla["help"] != null)
			{
				System.Console.Write(this.GetHelpMessage());
				return;
			}

			BuildFile bf = this.GetBuildFile(cla["bf"]);

			List<IBuildFileElement> elements = new List<IBuildFileElement>();

			if (cla["bc"] == null)
			{
				if(bf.DefaultComponents == null)
					elements = (List<IBuildFileElement>)bf.RootElements;
				else
					elements = (List<IBuildFileElement>)bf.GetBuildComponentsWithRootActions((List<string>)bf.DefaultComponents);
			}
			else
			{
				if(cla["bc"].Length == 0)
					throw new CamBuildConsoleException("Argument 'bc' must be followed by a list of components to be built.");

				if (cla["bc"] == "!all")
					elements = (List<IBuildFileElement>)bf.RootElements;
				else
				{
					List<string> comps = this.GetBuildComponents(cla["bc"]);

					elements = (List<IBuildFileElement>)bf.GetBuildComponentsWithRootActions(comps);
				}
			}

			if (cla["prop"] != null)
			{
				SetProperties(bf);
			}

			this.SetRebuild(cla["rebuild"], bf);

			if (cla["silent"] == null)
				Logging.Instance.Loggers.Add(new ConsoleLogger());

			this.AddLoggers(cla["log"], cla["bf"]);

			System.Console.WriteLine("[" + DateTime.Now.ToString("HH:mm:ss.fff") + "] Executing build file '" + cla["bf"] + "'.");
			System.Console.WriteLine();

			new BuildFileElementExecutor().ExecuteElements(elements);

			System.Console.WriteLine();
			System.Console.WriteLine("[" + DateTime.Now.ToString("HH:mm:ss.fff") + "] Execution completed.");
		}

		private List<string> GetBuildComponents(string arg)
		{
			List<string> comps = new List<string>();

			foreach (string comp in arg.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
			{
				comps.Add(comp.Trim());
			}

			return comps;
		}

		private void SetRebuild(string arg, BuildFile bf)
		{
			if (arg != null)
			{
				// rebuild specified components
				if (arg != "true")	// cla[string] returns "true" if argument is merely present
				{
					foreach (string comp in arg.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
					{
						bf.ForceRebuild(comp.Trim());
					}
				}
				else // rebuild all components
				{
					bf.ForceRebuildAll();
				}
			}
		}

		private void AddLoggers(string arg, string buildFileArg)
		{
			if (arg != null)
			{
				List<string> logSplit = new List<string>(arg.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries));

				foreach (string logger in logSplit)
				{
					if (!Logging.Instance.ContainsLogger(logger.Trim()))
					{
						switch (logger)
						{
							case "console":
								Logging.Instance.Loggers.Add(new ConsoleLogger());
								break;

							case "xml":
								string logFile = "";

								if (File.Exists(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + buildFileArg))
									logFile = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(buildFileArg) + ".log.xml";
								else
									logFile = Path.GetDirectoryName(buildFileArg) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(buildFileArg) + ".log.xml";
								Logging.Instance.Loggers.Add(new XmlFileLogger(logFile, buildFileArg));
								break;
						}
					}
				}
			}
		}

		private void SetProperties(BuildFile bf)
		{
			string[] propSetters = cla["prop"].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

			foreach (string propSetter in propSetters)
			{
				bf.SetPropertyValue(propSetter.Split(new char[] { '=' })[0], propSetter.Split(new char[] { '=' })[1]);
			}
		}
		
		private BuildFile GetBuildFile(string arg)
		{
			if (arg == null)
				throw new CamBuildConsoleException("Argument '-bf' is required.");

			if (arg.Length == 0)
				throw new CamBuildConsoleException("Argument '-bf' must be followed by a value (path to build file).");

			if (!File.Exists(arg))
				throw new CamBuildConsoleException("Argument '-bf' specifies an invalid path or non-existing file.");

			BuildFile bf = new BuildFile();
			bf.LoadXmlFile(arg);

			return bf;
		}

		private string GetHelpMessage()
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendLine("| Command-line argument reference:");
			sb.AppendLine("------------------------------------------------------------");

			sb.AppendLine();
			sb.AppendLine(this.GetHelpLine("-bf:<file>", "[R] Execute specified build file"));
			sb.AppendLine(this.GetHelpLine("-bc:[<comp1>[;<comp2>;...]]", "[O] Build specified component(s); default built if not set"));
			sb.AppendLine(this.GetHelpLine("-bc:!all", "[O] Build all components"));

			sb.AppendLine();
			sb.AppendLine(this.GetHelpLine("-rebuild", "[O] Force rebuild of all components"));
			sb.AppendLine(this.GetHelpLine("-rebuild:[[<comp1>[;<comp2>,...]]", "[O] Force rebuild of specified components"));
			sb.AppendLine(this.GetHelpLine("-prop:<prop>=<value>[;...]", "[O] Set property named <prop> to specified <value>"));

			sb.AppendLine();
			sb.AppendLine(this.GetHelpLine("-log:<type1>[;<type2>;...]", "[O] Add loggers (possible: 'console', 'xml')"));

			sb.AppendLine();
			sb.AppendLine(this.GetHelpLine("-help", "[O] Display this help message"));
			sb.AppendLine(this.GetHelpLine("-silent", "[O] Prevent logging to the console"));

			return sb.ToString();
		}

		private string GetHelpLine(string arg, string description)
		{
			return arg.PadRight(35, ' ') + description;
		}

	}
}
