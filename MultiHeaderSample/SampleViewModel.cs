using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace MultiHeaderSample
{
    public class SampleViewModel : ViewModel.BaseViewModel
    {
        DataTable dataTable;
        public DataView TEST
        {
            get
            {
                if (dataTable == null)
                {
                    dataTable = new DataTable();
                    dataTable.RowChanged += DataTable_RowChanged;
                    dataTable.RowDeleted += DataTable_RowChanged;
                    dataTable.Columns.Add(new DataColumn() { ColumnName = "T", DataType = typeof(double) });
                    dataTable.Columns.Add(new DataColumn() { ColumnName = "TT", DataType = typeof(double) });
                    
                    for (int i = 0; i < 27; i++)
                    {
                        DataRow dr = dataTable.NewRow();
                        dr[0] = i * 100;
                        dr[1] = i * 1000;
                        dataTable.Rows.Add(dr);
                    }
                }
                
                return dataTable.DefaultView;
            }
        }

        private void DataTable_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            FirePropertyChanged(nameof(TestValue3));
        }

        private ICommand? testCommand;
        public ICommand TestCommand
        {
            get { return testCommand ?? (testCommand = new DelegateCommand(TESTCommand)); }
        }

        public void TESTCommand()
        {
            MessageBox.Show("Command Excute");
        }

        public double TestValue3
        {
            get
            {
                double sample = 0;
                foreach (DataRow dr in dataTable.Rows)
                {
                    double temp = 0;
                    double.TryParse(dr["T"].ToString(), out temp);
                    sample += temp;
                }
                return sample;
            }
        }

        public string TestValue { get { return "R3C3 - Binding Test"; } }

        public double TestValue2 { get { return 1235465.234; } }
    }
}
