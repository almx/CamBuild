using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CamBuild.Core.Exceptions;

namespace CamBuild.Core
{
	public class BuildComponent : IBuildFileElement
	{
		private XmlElement xe;
		private List<IAction> actions = new List<IAction>();
		private BuildFile parentBuildFile;

		public BuildFile ParentBuildFile
		{
			get { return parentBuildFile; }
			set { parentBuildFile = value; }
		}

		public string Name
		{
			get { return this.xe.Attributes["name"].Value; }
		}

		public ICollection<string> Dependencies
		{
			get
			{
				List<string> deps = new List<string>();

				XmlAttribute xa = this.xe.Attributes["dependencies"];

				if (xa != null && xa.Value.Length > 0)
				{
					string[] tokens = xa.Value.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

					foreach (string token in tokens)
					{
						deps.Add(token.Trim());
					}
				}

				return deps;
			}
		}

		private void ParseActions()
		{
			XmlDocument xd = new XmlDocument();
			xd.LoadXml(this.xe.OuterXml);

			List<IBuildFileElement> elements = (List<IBuildFileElement>)BuildFileElementParser.Parse(xd, this.ParentBuildFile);

			foreach (IBuildFileElement element in elements)
			{
				if (element is IAction)
				{
					this.actions.Add((IAction)element);
				}
			}
		}

		public ICollection<IAction> Actions
		{
			get { return this.actions; }
		}

		public BuildComponent(XmlElement xe, BuildFile bf)
		{
			this.Validate(xe);
			this.xe = xe;

			this.ParentBuildFile = bf;
			this.ParseActions();
		}

		private void Validate(XmlElement xe)
		{
			if (xe.Name != "BuildComponent")
				throw new BuildFileParseException("Supplied XmlElement object does not have a BuildComponent as root node", xe);

			if (xe.Attributes["name"] == null)
				throw new BuildFileParseException("Supplied BuildComponent node does not have a 'name' attribute", xe);
		}
	}
}
