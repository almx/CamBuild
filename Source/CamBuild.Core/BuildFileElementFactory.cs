using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace CamBuild.Core
{
	public class BuildFileElementFactory
	{
		public static IBuildFileElement Create(XmlElement xe, BuildFile bf)
		{
			if (xe == null)
				return null;

			IBuildFileElement element = null;

			if (xe.Name == "BuildComponent")
			{
				element = new BuildComponent(xe, bf);
			}
			else if (xe.Name == "Property")
			{
				element = new Property(xe.Attributes["name"].Value, xe.InnerText);
				element.ParentBuildFile = bf;
			}
			else // assume that element is a BaseAction
			{
				element = ActionFactory.Create(xe);
				element.ParentBuildFile = bf;
			}

			return element;
		}
	}
}
