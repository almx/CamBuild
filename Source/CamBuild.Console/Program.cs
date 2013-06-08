using System;
using System.Collections.Generic;
using System.Text;
using CamBuild.Console.Exceptions;

namespace CamBuild.Console
{
	public class Program
	{
		public static void Main(string[] args)
		{
			//List<string> al = new List<string>();
			//al.Add(@"-bf:C:\Temp\TestTool.xml");
			//al.Add(@"-bc:TestLibrary,JavaApp");
			//al.Add(@"-prop:OutputDir=C:\Temp\NewDir");
			//al.Add(@"-log:xml");
			//al.Add(@"-rebuild");
			//args = new string[al.Count];
			//al.CopyTo(args, 0);

			try
			{
				new CamBuildConsole(new CommandLineArguments(args));
			}
			catch (CamBuildConsoleException ex)
			{
				System.Console.WriteLine(ex.Message);				
			}
			
		}
	}
}
