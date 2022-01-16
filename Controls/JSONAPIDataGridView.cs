using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NPOI.SS.Formula.Functions;
using System.Data;

namespace JSONAPI.Controls
{
    public static class JSONAPIDataGridView
    {

        public static void MakeSortable<T>(this DataGridView dataGridView, BindingSource bs, IEnumerable<T> dataSource, SortOrder defaultSort = SortOrder.Ascending, SortOrder initialSort = SortOrder.None)
        {

            var itemType = typeof(T);
            Dictionary<int, SortOrder> previousSortOrderDictionary = new Dictionary<int, SortOrder>();
            var sortProviderDictionary = new Dictionary<int, Func<SortOrder, IEnumerable<T>>>();
            bs.DataSource = dataSource; 
            dataGridView.DataSource = bs;
            foreach (DataGridViewColumn c in dataGridView.Columns)
            {
                object Provider(T info) => itemType.GetProperty(c.Name)?.GetValue(info);
                sortProviderDictionary[c.Index] = so => so != defaultSort ?
                    dataSource.OrderByDescending<T, object>(Provider) :
                    dataSource.OrderBy<T, object>(Provider);
                previousSortOrderDictionary[c.Index] = initialSort;
            }

            async Task DoSort(int index)
            {

                switch (previousSortOrderDictionary[index])
                {
                    case SortOrder.Ascending:
                        previousSortOrderDictionary[index] = SortOrder.Descending;
                        break;
                    case SortOrder.None:
                    case SortOrder.Descending:
                        previousSortOrderDictionary[index] = SortOrder.Ascending;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                IEnumerable<T> sorted = null;
                dataGridView.Cursor = Cursors.WaitCursor;
                dataGridView.Enabled = false;
                await Task.Run(() => sorted = (IEnumerable<T>)sortProviderDictionary[index](previousSortOrderDictionary[index]).ToList());
                bs.DataSource = sorted;
                dataGridView.DataSource = bs;
                dataGridView.Refresh();
                dataGridView.Enabled = true;
                dataGridView.Cursor = Cursors.Default;

            }
            dataGridView.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(MakeColumnsSortable_DataBindingComplete);
            dataGridView.ColumnHeaderMouseClick += async (object sender, DataGridViewCellMouseEventArgs e) => await DoSort(index: e.ColumnIndex);
        }
        
        static void MakeColumnsSortable_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //Add this as an event on DataBindingComplete
            DataGridView dataGridView = sender as DataGridView;
            if (dataGridView == null)
            {
                var ex = new InvalidOperationException("This event is for a DataGridView type senders only.");
                ex.Data.Add("Sender type", sender.GetType().Name);
                throw ex;
            }

            foreach (DataGridViewColumn column in dataGridView.Columns)
                column.SortMode = DataGridViewColumnSortMode.Automatic;
        }
    }

}
