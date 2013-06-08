using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace CamBuild.Core
{
	public class Utility
	{
		public static ICollection<Type> GetTypesFromAssemblyFiles(string path, string interfaceName)
		{
			List<Type> types = new List<Type>();

			foreach (string file in Directory.GetFiles(path, "CamBuild*.dll"))
			{
				foreach (Type t in Assembly.LoadFrom(file).GetTypes())
				{
					if (t.GetInterface(interfaceName) != null)
						types.Add(t);
				}
			}

			return types;
		}

		public static IBuildFileElement ConvertBaseAction(IAction ba)
		{
			return (IBuildFileElement)ba;
		}

		public static IBuildFileElement ConvertBuildComponent(BuildComponent bc)
		{
			return (IBuildFileElement)bc;
		}

		public static ICollection<string> TokenizeSourceFilesString(string sourceFiles)
		{
			List<string> files = new List<string>();

			Regex regex = new Regex("\".+?\"");

			foreach (Match match in regex.Matches(sourceFiles))
			{
				files.Add(match.Value.Trim(new char[] { ' ', '\"' }));
			}

			return files;
		}

		public static ICollection<string> GetFileListFromWildcardString(string str)
		{
			string[] fList = Directory.GetFiles(Path.GetDirectoryName(str),
				Path.GetFileName(str),
				SearchOption.AllDirectories);

			return new List<string>(fList);

		}

		public static ICollection<Type> ActionTypesFromAssemblyFiles
		{
			get	{ return Utility.GetTypesFromAssemblyFiles(Path.GetDirectoryName(Utility.ExecutablePath), "IAction"); }
		}

		public static ICollection<Type> FunctionTypesFromAssemblyFiles
		{
			get	{ return Utility.GetTypesFromAssemblyFiles(Path.GetDirectoryName(Utility.ExecutablePath), "IFunction"); }
		}

		public static string ExecutablePath
		{
			get
			{
				string exePath = System.Windows.Forms.Application.ExecutablePath;
#if DEBUG
				exePath = @"E:\Alexander\Projects\CamBuild\Source\CamBuild.Core\bin\Debug\CamBuild.Core.dll";
#endif
				return exePath;
			}
		}


		public static string Timestamp
		{
			get
			{
				return DateTime.Now.ToString("HH:mm:ss.fff");
			}
		}

		public static string FullDateTimestamp
		{
			get
			{
				DateTime dt = DateTime.Now;

				return dt.ToString("s") + dt.ToString(".fff");
			}
		}

	}
}
