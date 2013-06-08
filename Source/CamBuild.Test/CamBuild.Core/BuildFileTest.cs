using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using CamBuild.Core;
using BestBrains.System;
using System.Xml;

namespace CamBuild.Test
{
	[TestFixture]
	public class BuildFileTest
	{
		[Test]
		public void ProjectNamePropertyTest()
		{
			BuildFile bf = new BuildFile();

			bf.LoadXmlDocument(TestData.XmlDocument);

			Assert.AreEqual("Test Project", bf.ProjectName);
		}

		[Test]
		public void DefaultComponentsPropertyTest()
		{
			BuildFile bf = new BuildFile();
			bf.LoadXmlDocument(TestData.XmlDocument);

			List<string> comps = (List<string>)bf.DefaultComponents;

			Assert.AreEqual("Core", comps[0]);
			Assert.AreEqual("TestComponent", comps[1]);
		}

		[Test]
		public void RootElementsPropertyTest()
		{
			List<IBuildFileElement> elements = (List<IBuildFileElement>)TestData.BuildFile.RootElements;

			Assert.AreEqual("Property", ((Property)elements[0]).GetType().Name);
			Assert.AreEqual("WriteConsole", ((IAction)elements[1]).GetType().Name);
			Assert.AreEqual("BuildComponent", ((BuildComponent)elements[2]).GetType().Name);
		}

		[Test]
		public void PropertiesPropertyTest()
		{
			List<Property> props = (List<Property>)TestData.BuildFile.Properties;

			Assert.AreEqual(1, props.Count);
			Assert.AreEqual("OutputDir", props[0].Name);
			Assert.AreEqual(@"C:\Temp\TestTool", props[0].Value);
		}

		[Test]
		public void GetPropertyValueTest()
		{
			BuildFile bf = TestData.BuildFile;

			Assert.AreEqual(@"C:\Temp\TestTool", bf.GetPropertyValue("OutputDir"));
		}

		[Test]
		public void SetPropertyValueTest()
		{
			BuildFile bf = TestData.BuildFile;

			bf.SetPropertyValue("OutputDir", "TestSetOutputDir");

			Assert.AreEqual(@"TestSetOutputDir", bf.GetPropertyValue("OutputDir"));

			Assert.AreEqual(false, bf.SetPropertyValue("nonexistantproperty", "TestSetOutputDir"));
		}

		[Test]
		public void RootActionsPropertyTest()
		{
			List<IAction> actions = (List<IAction>)TestData.BuildFile.RootActions;

			Assert.AreEqual(1, actions.Count);
			Assert.AreEqual("WriteConsole", (actions[0]).GetType().Name);
		}

		[Test]
		public void GetBuildComponentTest()
		{
			BuildFile bf = TestData.BuildFile;

			BuildComponent bc1 = bf.GetBuildComponent("Core");
			Assert.AreEqual(bc1.Name, "Core");

			BuildComponent bc2 = bf.GetBuildComponent("TestComponent");
			Assert.AreEqual(bc2.Name, "TestComponent");
			Assert.AreEqual(1, bc2.Actions.Count);
		}

		[Test]
		public void GetBuildComponentsTest()
		{
			BuildFile bf = TestData.BuildFile;

			List<BuildComponent> components = (List<BuildComponent>)bf.GetBuildComponents(new List<string>(new string[] { "Core", "TestComponent" }));

			Assert.AreEqual(2, components.Count);
			Assert.AreEqual(components[0].Name, "Core");
			Assert.AreEqual(components[1].Name, "TestComponent");
		}

		[Test]
		public void GetBuildComponentWithRootActionsTest()
		{
			BuildFile bf = TestData.BuildFile;

			List<IBuildFileElement> elements = (List<IBuildFileElement>)bf.GetBuildComponentWithRootActions("Core");

			Assert.AreEqual(2, elements.Count);
			Assert.AreEqual(((BuildComponent)elements[1]).Name, "Core");
		}

		[Test]
		public void GetBuildComponentsWithRootActionsTest()
		{
			BuildFile bf = TestData.BuildFile;

			List<IBuildFileElement> elements = (List<IBuildFileElement>)bf.GetBuildComponentsWithRootActions(new List<string>(new string[] { "Core", "TestComponent" }));

			Assert.AreEqual(3, elements.Count);
			Assert.AreEqual(((BuildComponent)elements[1]).Name, "Core");
			Assert.AreEqual(((BuildComponent)elements[2]).Name, "TestComponent");
		}
		
	}
}
