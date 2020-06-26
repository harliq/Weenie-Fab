using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WeenieFab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // public int SelectedItemID { get; set; }
        public DataTable integerDataTable = new DataTable();

        public MainWindow()
        {
            InitializeComponent();
            CreateWeenieTypeList();
            CreateIntegerLists();
            CreateDataTable();
            dgInt32.DataContext = integerDataTable;

        }

        private void btnAddInt32_Click(object sender, RoutedEventArgs e)
        {
            // search for duplicate property

            if (SearchForDuplicateProps(integerDataTable, cbInt32Props.SelectedIndex.ToString()))
            {
                MessageBox.Show("Property Already Exits");
            }
            else
            {
                string[] description = cbInt32Props.Text.Split(" ");
                DataRow dr = integerDataTable.NewRow();
                dr[0] = cbInt32Props.SelectedIndex.ToString();
                dr[1] = tbValue.Text;
                dr[2] = description[1];

                integerDataTable.Rows.Add(dr);
            }

        }

        private static bool SearchForDuplicateProps(DataTable tempTable, string searchProp)
        {
            bool found = false;

            foreach (DataRow row in tempTable.Rows)
            {
                if (row[0].ToString() == searchProp)
                    found = true;
                else
                    found = false;
            }

            return found;
        }

        internal class FoundItem
        {
            public int RecordID { get; set; }
            public string RecordName { get; set; }
        }

        public void CreateDataTable()
        {
            integerDataTable.Columns.Add(new DataColumn("Property"));
            integerDataTable.Columns.Add(new DataColumn("Value"));
            integerDataTable.Columns.Add(new DataColumn("Description"));
        }

        private void btnInt32Remove_Click(object sender, RoutedEventArgs e)
        {
            var index = dgInt32.SelectedIndex;
            DataGridRow currentRowIndex = dgInt32.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
            DataRow dr = integerDataTable.Rows[currentRowIndex.GetIndex()];
            dr.Delete();

            integerDataTable.AcceptChanges();
        }

        private void btnUpdateInt32_Click_1(object sender, RoutedEventArgs e)
        {
            var index = dgInt32.SelectedIndex;
            DataGridRow currentRowIndex = dgInt32.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
            DataRow dr = integerDataTable.Rows[currentRowIndex.GetIndex()];


            string[] description = cbInt32Props.Text.Split(" ");
            
            dr[0] = cbInt32Props.SelectedIndex.ToString();
            dr[1] = tbValue.Text;
            dr[2] = description[1];


            integerDataTable.AcceptChanges();
        }

        public void CreateIntegerLists()
        {
            string filepath = "Int32Properties.txt";

            List<string> integerLists = new List<string>();
            foreach (string line in File.ReadLines(filepath))
            {
                integerLists.Add(line);
            }
            cbInt32Props.ItemsSource = integerLists;
            cbInt32Props.SelectedIndex = 1;
        }

        public void CreateWeenieTypeList()
        {
            string filepath = "WeenieTypes.txt";

            List<string> weenieTypeList = new List<string>();
            foreach (string line in File.ReadLines(filepath))
            {
                weenieTypeList.Add(line);
            }
            cbWeenieType.ItemsSource = weenieTypeList;
            cbWeenieType.SelectedIndex = 1;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void dgInt32_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index = dgInt32.SelectedIndex;
            DataGridRow currentRowIndex = dgInt32.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
            if (index + 1> integerDataTable.Rows.Count)
            {

            }
            else
            {
                DataRow dr = integerDataTable.Rows[currentRowIndex.GetIndex()];
                int cbindex = 0;
                Int32.TryParse(dr[0].ToString(), out cbindex);
                cbInt32Props.SelectedIndex = cbindex;
                tbValue.Text = dr[1].ToString();
            }
        }

        private void btnOpenSqlFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open SQL File";
            ofd.Filter = "SQL files|*.sql";
            ofd.InitialDirectory = @"C:\Ace";
            Nullable<bool> result = ofd.ShowDialog();

            if (result == true)
            {
                ReadSQLFile(ofd.FileName);

            }
        }

        private void btnSaveSqlFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Open Text File";
            sfd.Filter = "SQL files|*.sql";
            sfd.FileName = tbWCID.Text + $".sql";
            sfd.InitialDirectory = @"C:\Ace";

            Nullable<bool> result = sfd.ShowDialog();

            if (result == true)
            {

                WriteSQLFile(sfd.FileName);

            }

        }
    }
}
