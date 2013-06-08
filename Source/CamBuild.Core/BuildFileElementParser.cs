using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace CamBuild.Core
{
	public class BuildFileElementParser
	{
		public static ICollection<IBuildFileElement> Parse(XmlDocument xd, BuildFile parentBuildFile)
		{
			List<IBuildFileElement> elements = new List<IBuildFileElement>();

			foreach (XmlElement xe in xd.DocumentElement.ChildNodes)
			{
				IBuildFileElement element = BuildFileElementFactory.Create(xe, parentBuildFile);

				if (element != null)
					elements.Add(element);
			}

			return elements;
		}

	}
}
