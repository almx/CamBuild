namespace CamBuild.Gui
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.txtBuildFile = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.ofdBuildFile = new System.Windows.Forms.OpenFileDialog();
			this.groupOptions = new System.Windows.Forms.GroupBox();
			this.cListLogging = new System.Windows.Forms.CheckedListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.groupProperties = new System.Windows.Forms.GroupBox();
			this.dtProperties = new System.Windows.Forms.DataGridView();
			this.dtColName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dtColValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.checkForceRebuild = new System.Windows.Forms.CheckBox();
			this.groupBC = new System.Windows.Forms.GroupBox();
			this.cListBC = new System.Windows.Forms.CheckedListBox();
			this.radioBCSpec = new System.Windows.Forms.RadioButton();
			this.radioBCAll = new System.Windows.Forms.RadioButton();
			this.btnPerformBuild = new System.Windows.Forms.Button();
			this.ofdExe = new System.Windows.Forms.OpenFileDialog();
			this.groupOutput = new System.Windows.Forms.GroupBox();
			this.txtOutput = new System.Windows.Forms.TextBox();
			this.groupOptions.SuspendLayout();
			this.groupProperties.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtProperties)).BeginInit();
			this.groupBC.SuspendLayout();
			this.groupOutput.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtBuildFile
			// 
			this.txtBuildFile.Location = new System.Drawing.Point(70, 13);
			this.txtBuildFile.Name = "txtBuildFile";
			this.txtBuildFile.Size = new System.Drawing.Size(450, 20);
			this.txtBuildFile.TabIndex = 0;
			this.txtBuildFile.TextChanged += new System.EventHandler(this.txtBuildFile_TextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(52, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Build File:";
			// 
			// btnBrowse
			// 
			this.btnBrowse.Location = new System.Drawing.Point(527, 13);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(75, 23);
			this.btnBrowse.TabIndex = 2;
			this.btnBrowse.Text = "Browse...";
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// ofdBuildFile
			// 
			this.ofdBuildFile.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*\"";
			// 
			// groupOptions
			// 
			this.groupOptions.Controls.Add(this.cListLogging);
			this.groupOptions.Controls.Add(this.label2);
			this.groupOptions.Controls.Add(this.groupProperties);
			this.groupOptions.Controls.Add(this.checkForceRebuild);
			this.groupOptions.Controls.Add(this.groupBC);
			this.groupOptions.Enabled = false;
			this.groupOptions.Location = new System.Drawing.Point(15, 50);
			this.groupOptions.Name = "groupOptions";
			this.groupOptions.Size = new System.Drawing.Size(587, 340);
			this.groupOptions.TabIndex = 1;
			this.groupOptions.TabStop = false;
			this.groupOptions.Text = "Options";
			// 
			// cListLogging
			// 
			this.cListLogging.FormattingEnabled = true;
			this.cListLogging.Location = new System.Drawing.Point(353, 88);
			this.cListLogging.Name = "cListLogging";
			this.cListLogging.Size = new System.Drawing.Size(186, 64);
			this.cListLogging.TabIndex = 9;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(350, 72);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 13);
			this.label2.TabIndex = 8;
			this.label2.Text = "Logging:";
			// 
			// groupProperties
			// 
			this.groupProperties.Controls.Add(this.dtProperties);
			this.groupProperties.Location = new System.Drawing.Point(13, 161);
			this.groupProperties.Name = "groupProperties";
			this.groupProperties.Size = new System.Drawing.Size(554, 162);
			this.groupProperties.TabIndex = 7;
			this.groupProperties.TabStop = false;
			this.groupProperties.Text = "Properties";
			// 
			// dtProperties
			// 
			this.dtProperties.AllowUserToAddRows = false;
			this.dtProperties.AllowUserToDeleteRows = false;
			this.dtProperties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dtProperties.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dtColName,
            this.dtColValue});
			this.dtProperties.Location = new System.Drawing.Point(8, 19);
			this.dtProperties.MultiSelect = false;
			this.dtProperties.Name = "dtProperties";
			this.dtProperties.RowHeadersWidth = 18;
			this.dtProperties.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.dtProperties.Size = new System.Drawing.Size(540, 137);
			this.dtProperties.TabIndex = 4;
			// 
			// dtColName
			// 
			this.dtColName.HeaderText = "Name";
			this.dtColName.Name = "dtColName";
			this.dtColName.ReadOnly = true;
			this.dtColName.Width = 150;
			// 
			// dtColValue
			// 
			this.dtColValue.HeaderText = "Value";
			this.dtColValue.Name = "dtColValue";
			this.dtColValue.Width = 400;
			// 
			// checkForceRebuild
			// 
			this.checkForceRebuild.AutoSize = true;
			this.checkForceRebuild.Location = new System.Drawing.Point(353, 34);
			this.checkForceRebuild.Name = "checkForceRebuild";
			this.checkForceRebuild.Size = new System.Drawing.Size(92, 17);
			this.checkForceRebuild.TabIndex = 5;
			this.checkForceRebuild.Text = "Force Rebuild";
			this.checkForceRebuild.UseVisualStyleBackColor = true;
			// 
			// groupBC
			// 
			this.groupBC.Controls.Add(this.cListBC);
			this.groupBC.Controls.Add(this.radioBCSpec);
			this.groupBC.Controls.Add(this.radioBCAll);
			this.groupBC.Location = new System.Drawing.Point(13, 24);
			this.groupBC.Name = "groupBC";
			this.groupBC.Size = new System.Drawing.Size(320, 131);
			this.groupBC.TabIndex = 4;
			this.groupBC.TabStop = false;
			this.groupBC.Text = "Build Components";
			// 
			// cListBC
			// 
			this.cListBC.Enabled = false;
			this.cListBC.FormattingEnabled = true;
			this.cListBC.Location = new System.Drawing.Point(117, 20);
			this.cListBC.Name = "cListBC";
			this.cListBC.Size = new System.Drawing.Size(186, 94);
			this.cListBC.TabIndex = 3;
			// 
			// radioBCSpec
			// 
			this.radioBCSpec.AutoSize = true;
			this.radioBCSpec.Location = new System.Drawing.Point(12, 25);
			this.radioBCSpec.Name = "radioBCSpec";
			this.radioBCSpec.Size = new System.Drawing.Size(98, 17);
			this.radioBCSpec.TabIndex = 2;
			this.radioBCSpec.Text = "Build Specified:";
			this.radioBCSpec.UseVisualStyleBackColor = true;
			this.radioBCSpec.CheckedChanged += new System.EventHandler(this.radioBCSpec_CheckedChanged);
			// 
			// radioBCAll
			// 
			this.radioBCAll.AutoSize = true;
			this.radioBCAll.Checked = true;
			this.radioBCAll.Location = new System.Drawing.Point(12, 48);
			this.radioBCAll.Name = "radioBCAll";
			this.radioBCAll.Size = new System.Drawing.Size(62, 17);
			this.radioBCAll.TabIndex = 1;
			this.radioBCAll.TabStop = true;
			this.radioBCAll.Text = "Build All";
			this.radioBCAll.UseVisualStyleBackColor = true;
			// 
			// btnPerformBuild
			// 
			this.btnPerformBuild.Enabled = false;
			this.btnPerformBuild.Location = new System.Drawing.Point(261, 545);
			this.btnPerformBuild.Name = "btnPerformBuild";
			this.btnPerformBuild.Size = new System.Drawing.Size(87, 23);
			this.btnPerformBuild.TabIndex = 3;
			this.btnPerformBuild.Text = "Perform Build";
			this.btnPerformBuild.UseVisualStyleBackColor = true;
			this.btnPerformBuild.Click += new System.EventHandler(this.btnPerformBuild_Click);
			// 
			// ofdExe
			// 
			this.ofdExe.FileName = "CamBuild.Console.exe";
			this.ofdExe.Filter = "CamBuild.Console.exe|CamBuild.Console.exe";
			// 
			// groupOutput
			// 
			this.groupOutput.Controls.Add(this.txtOutput);
			this.groupOutput.Location = new System.Drawing.Point(13, 397);
			this.groupOutput.Name = "groupOutput";
			this.groupOutput.Size = new System.Drawing.Size(588, 139);
			this.groupOutput.TabIndex = 7;
			this.groupOutput.TabStop = false;
			this.groupOutput.Text = "Output";
			// 
			// txtOutput
			// 
			this.txtOutput.Location = new System.Drawing.Point(15, 20);
			this.txtOutput.Multiline = true;
			this.txtOutput.Name = "txtOutput";
			this.txtOutput.ReadOnly = true;
			this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtOutput.Size = new System.Drawing.Size(554, 113);
			this.txtOutput.TabIndex = 0;
			this.txtOutput.WordWrap = false;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(614, 573);
			this.Controls.Add(this.groupOutput);
			this.Controls.Add(this.btnPerformBuild);
			this.Controls.Add(this.btnBrowse);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtBuildFile);
			this.Controls.Add(this.groupOptions);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "CamBuild GUI";
			this.groupOptions.ResumeLayout(false);
			this.groupOptions.PerformLayout();
			this.groupProperties.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dtProperties)).EndInit();
			this.groupBC.ResumeLayout(false);
			this.groupBC.PerformLayout();
			this.groupOutput.ResumeLayout(false);
			this.groupOutput.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtBuildFile;
		private System.Windows.Forms.OpenFileDialog ofdBuildFile;
		private System.Windows.Forms.GroupBox groupOptions;
		private System.Windows.Forms.RadioButton radioBCAll;
		private System.Windows.Forms.RadioButton radioBCSpec;
		private System.Windows.Forms.GroupBox groupBC;
		private System.Windows.Forms.Button btnPerformBuild;
		private System.Windows.Forms.CheckBox checkForceRebuild;
		private System.Windows.Forms.CheckedListBox cListBC;
		private System.Windows.Forms.GroupBox groupProperties;
		private System.Windows.Forms.CheckedListBox cListLogging;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DataGridView dtProperties;
		private System.Windows.Forms.DataGridViewTextBoxColumn dtColName;
		private System.Windows.Forms.DataGridViewTextBoxColumn dtColValue;
		private System.Windows.Forms.OpenFileDialog ofdExe;
		private System.Windows.Forms.GroupBox groupOutput;
		private System.Windows.Forms.TextBox txtOutput;
	}
}

