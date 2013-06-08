using System;
using System.Collections.Generic;
using System.Text;
using CamBuild.Core.Exceptions;

namespace CamBuild.Core
{
	public class FunctionFactory
	{
		public static IFunction Create(string funcName)
		{
			List<Type> types = (List<Type>)Utility.FunctionTypesFromAssemblyFiles;

			foreach (Type type in types)
			{
				if (type.Name == funcName)
				{
					IFunction bf = (IFunction)type.Assembly.CreateInstance(type.FullName);

					return bf;
				}
			}

			throw new UnrecognizedFunctionException(funcName, "Function was not found in searched DLL plugin files");

		}
	}
}
