using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Reflection;
using System.IO;

namespace CamBuild.Test
{
	public class TestUtility
	{
		public static string TempDir
		{
			get
			{
				//return Path.GetTempPath();
				return @"C:\Temp\";
			}
		}

		public static void CreateFile(string fileName)
		{
			File.Delete(fileName);

			FileStream fs = new FileStream(fileName, FileMode.CreateNew);
			fs.Write(new byte[] { 65 }, 0, 1);
			fs.Close();
		}
	}
}
