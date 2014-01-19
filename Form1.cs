using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Syncfusion.Windows.Forms.Grid;

namespace DatabaseExplorer
{
	public partial class Form1 : Form
	{
		protected string connectionString = "data source=localhost;initial catalog=AdventureWorks2008;integrated security=SSPI";

		public Form1()
		{
			InitializeComponent();
			InitializeAlternatingColors();
			InitializeTableList();
		}

		protected void InitializeAlternatingColors()
		{
			gridTableList.Grid.PrepareViewStyleInfo += new GridPrepareViewStyleInfoEventHandler(grid_PrepareViewStyleInfo);
			gridTableData.Grid.PrepareViewStyleInfo += new GridPrepareViewStyleInfoEventHandler(grid_PrepareViewStyleInfo);
		}

		private void grid_PrepareViewStyleInfo(object sender, GridPrepareViewStyleInfoEventArgs e)
		{
			if (e.RowIndex > 0)
			{
				e.Style.BackColor = (e.RowIndex % 2 == 1 ? Color.White : Color.FromArgb(255, 240, 255));
			}
		}		

		protected void InitializeTableList()
		{
			List<string> tableList;

			using (var conn = BackEnd.openConnection(connectionString))
			{
				tableList = BackEnd.getTablesAsGenericList(conn);
			}

			var tableNameList = new List<TableName>();
			tableList.ForEach(t => tableNameList.Add(new TableName() { Name = t }));
			gridTableList.DisplayMember = "Name";
			gridTableList.ValueMember = "Name";
			gridTableList.DataSource = tableNameList;
		}

		private void gridTableList_SelectedValueChanged(object sender, EventArgs e)
		{
			if (gridTableList.SelectedValue != null)
			{
				string tableName = gridTableList.SelectedValue.ToString();
				DataTable dt;

				using (var conn = BackEnd.openConnection(connectionString))
				{
					Cursor = Cursors.WaitCursor;
					var data = BackEnd.loadData(conn, tableName);
					dt = BackEnd.toDataTable(data.Item1, data.Item2);
					Cursor = Cursors.Arrow;
				}

				gridTableData.DataSource = dt;
			}
		}

		private void gridTableList_Click(object sender, EventArgs e)
		{

		}
	}

	public class TableName
	{
		public string Name { get; set; }
	}
}
