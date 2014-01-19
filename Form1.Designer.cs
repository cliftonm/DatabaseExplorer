namespace DatabaseExplorer
{
	partial class Form1
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
			this.gridTableList = new Syncfusion.Windows.Forms.Grid.GridListControl();
			this.gridTableData = new Syncfusion.Windows.Forms.Grid.GridListControl();
			((System.ComponentModel.ISupportInitialize)(this.gridTableList)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridTableData)).BeginInit();
			this.SuspendLayout();
			// 
			// gridTableList
			// 
			this.gridTableList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gridTableList.BackColor = System.Drawing.SystemColors.Control;
			this.gridTableList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.gridTableList.ItemHeight = 17;
			this.gridTableList.Location = new System.Drawing.Point(12, 12);
			this.gridTableList.MultiColumn = false;
			this.gridTableList.Name = "gridTableList";
			this.gridTableList.Properties.BackgroundColor = System.Drawing.SystemColors.Window;
			this.gridTableList.Properties.MarkColHeader = false;
			this.gridTableList.Properties.MarkRowHeader = false;
			this.gridTableList.SelectedIndex = -1;
			this.gridTableList.Size = new System.Drawing.Size(158, 286);
			this.gridTableList.TabIndex = 0;
			this.gridTableList.TopIndex = 0;
			this.gridTableList.SelectedValueChanged += new System.EventHandler(this.gridTableList_SelectedValueChanged);
			this.gridTableList.Click += new System.EventHandler(this.gridTableList_Click);
			// 
			// gridTableData
			// 
			this.gridTableData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridTableData.BackColor = System.Drawing.SystemColors.Control;
			this.gridTableData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.gridTableData.ItemHeight = 17;
			this.gridTableData.Location = new System.Drawing.Point(176, 12);
			this.gridTableData.MultiColumn = true;
			this.gridTableData.Name = "gridTableData";
			this.gridTableData.Properties.BackgroundColor = System.Drawing.SystemColors.Window;
			this.gridTableData.Properties.MarkColHeader = false;
			this.gridTableData.Properties.MarkRowHeader = false;
			this.gridTableData.SelectedIndex = -1;
			this.gridTableData.Size = new System.Drawing.Size(340, 286);
			this.gridTableData.TabIndex = 1;
			this.gridTableData.TopIndex = 0;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(528, 310);
			this.Controls.Add(this.gridTableData);
			this.Controls.Add(this.gridTableList);
			this.Name = "Form1";
			this.Text = "Database Explorer";
			((System.ComponentModel.ISupportInitialize)(this.gridTableList)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridTableData)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Syncfusion.Windows.Forms.Grid.GridListControl gridTableList;
		private Syncfusion.Windows.Forms.Grid.GridListControl gridTableData;
	}
}

