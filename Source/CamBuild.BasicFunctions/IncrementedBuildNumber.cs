using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using CamBuild.Core;

namespace CamBuild.BasicFunctions
{
	public class IncrementedBuildNumber : IFunction
	{
		// args[0]: Path
		// args[1]: Mask - must contain dots (.)
		public string GetResult(ICollection<string> args)
		{
			List<string> argList = (List<string>)args;

			if (Directory.Exists(argList[0]))
			{
				List<string> dirs = new List<string>(Directory.GetDirectories(argList[0], argList[1], SearchOption.TopDirectoryOnly));

				if (dirs.Count == 0)
				{
					return this.Get10Dir(argList[0], argList[1]);
				}

				dirs.Sort(CompareDirs);

				string lastDir = dirs[dirs.Count - 1];

				int lastBuildNumber = Convert.ToInt32(lastDir.Substring(lastDir.LastIndexOf('.') + 1));

				return lastDir.Substring(0, lastDir.LastIndexOf('.')) + '.' + Convert.ToString(lastBuildNumber + 1);
			}
			else
				return this.Get10Dir(argList[0], argList[1]);
		}

		private int CompareDirs(string dir1, string dir2)
		{
			int lastBuildNumberDir1 = Convert.ToInt32(dir1.Substring(dir1.LastIndexOf('.') + 1));
			int lastBuildNumberDir2 = Convert.ToInt32(dir2.Substring(dir2.LastIndexOf('.') + 1));

			return lastBuildNumberDir1 - lastBuildNumberDir2;
		}

		private string Get10Dir(string dir, string mask)
		{
			string baseDir = mask.Replace('*', '0');
			baseDir = "1" + baseDir.Substring(1);

			return dir + Path.DirectorySeparatorChar + baseDir;
		}

		public string Description
		{
			get { return "Searches a given path for directories matching supplied mask, returns \"incremented\" number"; }
		}
	}
}
