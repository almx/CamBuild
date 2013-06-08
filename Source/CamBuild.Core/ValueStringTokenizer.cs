using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CamBuild.Core
{
	public class ValueStringTokenizer
	{
		public static ICollection<ValueToken> TokenizeString(string str)
		{
			List<ValueToken> tokens = new List<ValueToken>();

			StringReader sr = new StringReader(str);

			string text = "";

			while (sr.Peek() != -1)
			{
				char c = (char)sr.Read();

				if (c == '[' && sr.Peek() == (int)'$' || sr.Peek() == (int)'#')
				{
					tokens.Add(new ValueToken(ValueTokenType.Text, text.TrimEnd(new char[] { '[' })));
					
					ValueTokenType valType = new ValueTokenType();

					if(sr.Peek() == (int)'$')
						valType = ValueTokenType.Function;
					else if(sr.Peek() == (int)'#')
						valType = ValueTokenType.Property;

					// consume '$' or '#'
					sr.Read();

					string val = "";

					while (true)
					{
						val += (char)sr.Read();

						if (valType == ValueTokenType.Function && sr.Peek() == ')')
						{
							sr.Read();

							if (sr.Peek() == ']')
							{
								val += ")";

								break;
							}
						}
						else if (valType == ValueTokenType.Property && sr.Peek() == ']')
						{
							break;
						}
					}

					// consume ']'
					sr.Read();

					tokens.Add(new ValueToken(valType, val));
					text = "";
				}
				else
				{
					text += c;
				}
			}

			tokens.Add(new ValueToken(ValueTokenType.Text, text));

			List<ValueToken> newTokens = new List<ValueToken>();

			foreach (ValueToken token in tokens)
			{
				if (token.Text.Length > 0)
					newTokens.Add(token);
			}

			sr.Close();

			return newTokens;
		}
	}
}
