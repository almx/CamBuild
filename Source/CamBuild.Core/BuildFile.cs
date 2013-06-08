using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Reflection;
using CamBuild.Core.Exceptions;

namespace CamBuild.Core
{
	public class BuildFile
	{
		private XmlDocument xd;
		private List<IBuildFileElement> rootElements = new List<IBuildFileElement>();
		private List<Property> properties = new List<Property>();

		public string ProjectName
		{
			get
			{
				return this.xd.SelectSingleNode("/CamBuildProject").Attributes["name"].Value;
			}
		}

		public ICollection<string> DefaultComponents
		{
			get
			{
				string components = "";

				if (this.xd.SelectSingleNode("/CamBuildProject").Attributes["defaultcomponents"] != null)
					components = this.xd.SelectSingleNode("/CamBuildProject").Attributes["defaultcomponents"].Value;
				else
					return null;

				List<string> compList = new List<string>();

				foreach (string comp in components.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					compList.Add(comp.Trim());
				}

				return compList;
			}
		}

		public ICollection<IBuildFileElement> RootElements
		{
			get	{ return this.rootElements;	}
		}

		public ICollection<Property> Properties
		{
			get	{ return this.properties; }
		}

		public string GetPropertyValue(string propertyName)
		{
			foreach (Property prop in this.Properties)
			{
				if (prop.Name == propertyName)
					return prop.Value;
			}

			return null;
		}

		public bool SetPropertyValue(string propertyName, string value)
		{
			foreach (Property prop in this.Properties)
			{
				if (prop.Name == propertyName)
				{
					prop.Value = ValueStringEvaluator.Evaluate(value, this);
					return true;
				}
			}

			return false;
		}

		public ICollection<IAction> RootActions
		{
			get
			{
				List<IAction> actions = new List<IAction>();

				foreach (IBuildFileElement element in this.RootElements)
				{
					if (element is IAction)
						actions.Add((IAction)element);
				}

				return actions;
			}
		}

		public ICollection<BuildComponent> BuildComponents
		{
			get
			{
				List<BuildComponent> comps = new List<BuildComponent>();

				foreach (IBuildFileElement element in this.RootElements)
				{
					if (element is BuildComponent)
						comps.Add((BuildComponent)element);
				}

				return comps;
			}
		}

		public void ForceRebuild(string componentName)
		{
			BuildComponent bc = this.GetBuildComponent(componentName);

			foreach (IAction ba in bc.Actions)
			{
				if(ba is ICompilerAction)
					((ICompilerAction)ba).ForceRebuild = true;
			}
		}

		public void ForceRebuildAll()
		{
			foreach(IBuildFileElement element in this.RootElements)
			{
				if(element is BuildComponent)
					this.ForceRebuild(((BuildComponent)element).Name);
			}
		}


		#region GetBuildComponent* methods

		public BuildComponent GetBuildComponent(string componentName)
		{
			foreach (IBuildFileElement element in this.rootElements)
			{
				if(element is BuildComponent)
					if (((BuildComponent)element).Name == componentName)
						return (BuildComponent)element;
			}

			return null;
		}

		public ICollection<IBuildFileElement> GetBuildComponentWithRootActions(string componentName)
		{
			BuildComponent bc = this.GetBuildComponent(componentName);

			List<IBuildFileElement> elements = new List<IBuildFileElement>();
			elements.AddRange(((List<IAction>)this.RootActions).ConvertAll(new Converter<IAction, IBuildFileElement>(Utility.ConvertBaseAction)));

			elements.Add((IBuildFileElement)bc);

			return elements;
		}

		public ICollection<BuildComponent> GetBuildComponents(ICollection<string> components)
		{
			List<BuildComponent> bcList = new List<BuildComponent>();

			foreach (string comp in components)
			{
				bcList.Add(this.GetBuildComponent(comp));
			}

			return bcList;
		}

		public ICollection<IBuildFileElement> GetBuildComponentsWithRootActions(ICollection<string> components)
		{
			List<IBuildFileElement> elements = new List<IBuildFileElement>();

			elements.AddRange(((List<IAction>)this.RootActions).ConvertAll(new Converter<IAction, IBuildFileElement>(Utility.ConvertBaseAction)));

			foreach (string comp in components)
			{
				elements.Add(this.GetBuildComponent(comp));
			}

			return elements;
		}

		#endregion

		public void LoadXmlString(string xml)
		{
			this.xd = new XmlDocument();
			this.xd.LoadXml(xml);

			this.ValidateAndInitialize();
		}


		public void LoadXmlDocument(XmlDocument xd)
		{
			this.xd = xd;

			this.ValidateAndInitialize();
		}

		// TODO: not tested due to requirement of physical file
		public void LoadXmlFile(string path)
		{
			this.xd = new XmlDocument();
			this.xd.Load(path);

			this.ValidateAndInitialize();
		}

		private void ValidateAndInitialize()
		{
			this.Validate();
			this.Initialize();
		}

		private void Initialize()
		{
			this.rootElements.Clear();
			this.properties.Clear();

			foreach (XmlElement xe in this.xd.SelectNodes("/CamBuildProject/Property"))
			{
				Property prop = (Property)BuildFileElementFactory.Create(xe, this);
				this.properties.Add(prop);
				this.SetPropertyValue(prop.Name, ValueStringEvaluator.Evaluate(prop.Value, this));
			}

			this.rootElements = (List<IBuildFileElement>)BuildFileElementParser.Parse(this.xd, this);
		}

		private void Validate()
		{
			if (this.xd == null)
				throw new BuildFileParseException("Xml parsing error");

			XmlNodeList xnl = this.xd.SelectNodes("/CamBuildProject");

			if (xnl.Count == 0)
				throw new BuildFileParseException("Build file does not contain a CamBuildProject root element");

			if (xnl.Count > 1)
				throw new BuildFileParseException("Build file contains more than one CamBuildProject element");

			// TODO: Assert that properties are declared at the beginning of the file
			// TODO: Check for circular BuildComponent dependencies, clashing BuildComponent names + further verifications
			
		}			

	}
}
