using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using CamBuild.Core;
using CamBuild.Core.Exceptions;
using System.Xml;
using System.Diagnostics;

namespace CamBuild.Gui
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private void InitializeOptions()
		{
			cListBC.Items.Clear();
			dtProperties.Rows.Clear();
			cListLogging.Items.Clear();

			BuildFile bf = new BuildFile();
			bf.LoadXmlFile(txtBuildFile.Text);

			this.FillListBc(bf);
			this.FillListProperties(bf);
			this.FillLoggers();
		}

		private void FillLoggers()
		{
			cListLogging.Items.Add("xml");
		}

		private void FillListProperties(BuildFile bf)
		{
			// get "raw" property values from build file
			XmlDocument xd = new XmlDocument();
			xd.Load(txtBuildFile.Text);

			foreach (XmlElement xe in xd.SelectNodes("/CamBuildProject/Property"))
			{
				string[] propRow = new string[] { xe.Attributes["name"].Value, xe.InnerText };
				dtProperties.Rows.Add(propRow);
			}
		}

		private void FillListBc(BuildFile bf)
		{
			foreach (BuildComponent bc in bf.BuildComponents)
			{
				cListBC.Items.Add(bc.Name);

				if(bf.DefaultComponents != null)
					cListBC.SetItemChecked(cListBC.Items.Count - 1, bf.DefaultComponents.Contains(bc.Name));
			}
		}

		private bool IsValidBuildFile(string filePath)
		{
			if (!File.Exists(filePath))
				return false;

			try
			{
				BuildFile bf = new BuildFile();
				bf.LoadXmlFile(filePath);
			}
			catch (XmlException)
			{
				return false;
			}
			catch (BuildFileParseException)
			{
				return false;
			}

			return true;
		}

		private BuildFile GetBuildFile()
		{
			BuildFile bf = new BuildFile();

			try
			{
				bf.LoadXmlFile(txtBuildFile.Text);
			}
			catch (BuildFileParseException)
			{
				MessageBox.Show("Error parsing specified build file", "CamBuild");
				return null;
			}

			return bf;
		}

		private void PerformBuild()
		{
			StringWriter sw = this.RedirectConsoleLogging();

			try
			{
				BuildFile bf = this.GetBuildFile();

				if (bf == null)
					return;

				List<IBuildFileElement> elements = this.GetElements(bf);

				this.SetLoggers();

				this.SetRebuild(bf);

				this.SetProperties(bf);

				this.WriteLogHeader(txtBuildFile.Text);

				new BuildFileElementExecutor().ExecuteElements(elements);
			}
			catch (ActionNotExecutedException ex)	// TODO: Implement common CamBuildException base class
			{
				txtOutput.Text += "An exception was thrown upon action execution; message: " + ex.Message;
			}
			finally
			{
				txtOutput.Text += sw.ToString();
				this.WriteLogFooter();

				txtOutput.SelectionStart = txtOutput.Text.Length - 1;
				txtOutput.ScrollToCaret();

				this.CleanupConsoleLogging();
			}
		}

		private void WriteLogHeader(string buildFilePath)
		{
			txtOutput.Clear();

			txtOutput.Text += "[" + DateTime.Now.ToString("HH:mm:ss.fff") + "] Executing build file '" + buildFilePath + "'." + Environment.NewLine;
			txtOutput.Text += Environment.NewLine;
		}

		private void WriteLogFooter()
		{
			txtOutput.Text += Environment.NewLine;
			txtOutput.Text += "[" + DateTime.Now.ToString("HH:mm:ss.fff") + "] Execution completed." + Environment.NewLine;
		}

		private StringWriter RedirectConsoleLogging()
		{
			if(!Logging.Instance.ContainsLogger("console"))
				Logging.Instance.Loggers.Add(new ConsoleLogger());

			StringWriter sw = new StringWriter();
			Console.SetOut(sw);

			return sw;
		}

		private void CleanupConsoleLogging()
		{
			StreamWriter standardOutput = new StreamWriter(Console.OpenStandardOutput());
			standardOutput.AutoFlush = true;

			Console.SetOut(standardOutput);
		}

		private void SetProperties(BuildFile bf)
		{
			foreach (DataGridViewRow row in dtProperties.Rows)
			{
				bf.SetPropertyValue(row.Cells["dtColName"].Value.ToString(), row.Cells["dtColValue"].Value.ToString());
			}
		}

		private void SetRebuild(BuildFile bf)
		{
			if (checkForceRebuild.Checked)
			{
				bf.ForceRebuildAll();
			}
		}

		private void SetLoggers()
		{
			foreach (object item in cListLogging.CheckedItems)
			{
				if (item.ToString() == "xml" && !Logging.Instance.ContainsLogger("xml"))
				{
					string xmlLog = Path.GetDirectoryName(txtBuildFile.Text) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(txtBuildFile.Text) + ".log.xml";
					Logging.Instance.Loggers.Add(new XmlFileLogger(xmlLog, txtBuildFile.Text));
				}
			}
		}

		private List<IBuildFileElement> GetElements(BuildFile bf)
		{
			List<IBuildFileElement> elements = new List<IBuildFileElement>();

			if (radioBCAll.Checked)
			{
				elements = (List<IBuildFileElement>)bf.RootElements;
			}
			else if (radioBCSpec.Checked)
			{
				List<string> comps = new List<string>();

				foreach (object item in cListBC.CheckedItems)
				{
					comps.Add(item.ToString());
				}

				elements = (List<IBuildFileElement>)bf.GetBuildComponentsWithRootActions(comps);
			}

			return elements;
		}

		private void radioBCSpec_CheckedChanged(object sender, EventArgs e)
		{
			cListBC.Enabled = radioBCSpec.Checked;
		}

		private void btnPerformBuild_Click(object sender, EventArgs e)
		{
			this.PerformBuild();
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			if (ofdBuildFile.ShowDialog() == DialogResult.OK)
			{
				txtBuildFile.Text = ofdBuildFile.FileName;
			}

		}

		private void txtBuildFile_TextChanged(object sender, EventArgs e)
		{
			this.EnableControls();

			if (groupOptions.Enabled)
				this.InitializeOptions();
		}

		private void EnableControls()
		{
			groupOptions.Enabled = this.IsValidBuildFile(txtBuildFile.Text);

			btnPerformBuild.Enabled = groupOptions.Enabled;
		}
	}
}