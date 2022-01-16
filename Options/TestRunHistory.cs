using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JSONAPI.Options
{
    public partial class frmTestRunHistory : Form
    {
        public frmTestRunHistory(List<Utilities.ExecutionResult.TestRun> runList)
        {
            InitializeComponent();
            this.Icon = Properties.Resources.jsonapi;
            AddEventHandlers();
            bsTestRunHistory.DataSource = runList;
            dgTestSuiteHistory.DataSource = bsTestRunHistory;
            dgTestSuiteHistory.Refresh();
        }

        public void AddEventHandlers()
        {
            this.dgTestSuiteHistory.CellContentClick += new DataGridViewCellEventHandler(this.dgTestSuiteHistory_CellContentClick);
            this.dgTestSuiteHistory.CellPainting += new DataGridViewCellPaintingEventHandler(this.dgTestSuiteHistory_CellPainting);
        }

        private void dgTestSuiteHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 & e.ColumnIndex > -1)
            {
                if (dgTestSuiteHistory[e.ColumnIndex, e.RowIndex] is DataGridViewButtonCell)
                {
                    var testRunList = (Utilities.ExecutionResult.TestRun)dgTestSuiteHistory.Rows[e.RowIndex].DataBoundItem;
                    var fieldList = testRunList.TestResultList;
                    frmFieldList tfForm = new frmFieldList(fieldList);
                    tfForm.TopMost = true;
                    tfForm.Show();
                }
            }
        }

        private void dgTestSuiteHistory_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 4)
                {
                    DataGridViewCell cell = this.dgTestSuiteHistory.Rows[e.RowIndex].Cells[e.ColumnIndex];

                    e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~(DataGridViewPaintParts.ContentForeground));
                    var r = e.CellBounds;
                    r.Inflate(-1, -1);
                    e.Graphics.FillRectangle(Brushes.CornflowerBlue, r);
                    e.Paint(e.CellBounds, DataGridViewPaintParts.ContentForeground);
                    e.Handled = true;
                    cell.ReadOnly = true;
                    cell.ToolTipText = "Click here to Test List History..";
                }
            }
            catch (Exception fe)
            {
                Utilities.Utilities.WriteLogItem("Error defining Button for dgTestSuiteHistory" + fe.ToString(), System.Diagnostics.TraceEventType.Error);
            }
        }
    }
}
