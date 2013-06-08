using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using NUnit.Framework;
using CamBuild.Core;
using BestBrains.System;

namespace CamBuild.Test
{
	[TestFixture]
	public class ValueStringTokenizerTest
	{
		[Test]
		public void TestTokenizeString()
		{
			string str = "Test$: [$DateTime(datetime)] test [#TestProperty] asdf";

			List<ValueToken> tokens = (List<ValueToken>)ValueStringTokenizer.TokenizeString(str);

			Assert.AreEqual(5, tokens.Count);
			Assert.AreEqual(ValueTokenType.Text, tokens[0].Type);
			Assert.AreEqual("Test$: ", tokens[0].Text);

			Assert.AreEqual(ValueTokenType.Function, tokens[1].Type); 
			Assert.AreEqual("DateTime(datetime)", tokens[1].Text);

			Assert.AreEqual(ValueTokenType.Property, tokens[3].Type);
			Assert.AreEqual("TestProperty", tokens[3].Text);

		}

	}
}