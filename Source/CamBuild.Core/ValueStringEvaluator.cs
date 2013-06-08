using System;
using System.Collections.Generic;
using System.Text;
using CamBuild.Core.Exceptions;

namespace CamBuild.Core
{
	public class ValueStringEvaluator
	{
		public static string Evaluate(string valueString, BuildFile parentBuildFile)
		{
			List<ValueToken> tokens = (List<ValueToken>)ValueStringTokenizer.TokenizeString(valueString);

			string eval = "";

			foreach (ValueToken token in tokens)
			{
				if (token.Type == ValueTokenType.Text)
				{
					eval += token.Text;
				}
				else if (token.Type == ValueTokenType.Function)
				{
					FunctionString fs = new FunctionString(token.Text);
					IFunction bf = FunctionFactory.Create(fs.Name);

					List<string> newArgs = new List<string>();

					foreach (string arg in fs.Args)
					{
						// TODO: This will cause a fatal error if function calls are nested
						newArgs.Add(ValueStringEvaluator.Evaluate(arg, parentBuildFile));
					}

					eval += bf.GetResult(newArgs);
				}
				else if (token.Type == ValueTokenType.Property)
				{
					if (parentBuildFile == null)
						throw new ValueStringEvaluatorException("Value string contains a property, " +
							"but supplied BuildFile object is null", valueString);

					eval += parentBuildFile.GetPropertyValue(token.Text);
				}
			}

			return eval;
		}


	}
}
