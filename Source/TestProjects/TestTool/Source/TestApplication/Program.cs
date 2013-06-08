using System;
using System.Collections.Generic;
using System.Text;
using TestLibrary;

namespace TestApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			TestClass tc = new TestClass();

			tc.Text = "Test";

			Console.WriteLine(tc.Text);

			Console.WriteLine(SampleClass.Add(2, 3));
		}
	}
}
