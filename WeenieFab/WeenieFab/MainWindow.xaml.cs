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
        public int SelectedItemID { get; set; }
        public DataTable integerDataTable = new DataTable();
        // public Dictionary<int, FoundItem> FoundItems { get; set; }
        // public List<string> integerLists { get; set; } = new List<string>();


        public MainWindow()
        {
            InitializeComponent();
            //CreateDictionary();
            CreateIntegerLists("Don't need");
            CreateDataTable();
            dgInt32.DataContext = integerDataTable;
            //cbWeenieType.ItemsSource = integerLists;
        }

        public class DataGridValues
        {
            public string Property { get; set; }
            public string Value { get; set; }
            public string Description { get; set; }
        }

        public void CreateDictionary()
        {
            IDictionary<int, string> dInt = new Dictionary<int, string>();

            dInt.Add(1, "Item Type");
            dInt.Add(2, "Creature Type");

            // cbInt32.ItemsSource = dInt;


            IDictionary<int, string> WeenieType = new Dictionary<int, string>();

            WeenieType.Add(0, "Undefined");
            WeenieType.Add(1, "Generic");
            WeenieType.Add(2, "Clothing");
            WeenieType.Add(3, "Missile Launcher");

            cbWeenieType.ItemsSource = WeenieType;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // search for duplicate property

            // var integerDataTable = as new DataTable; DataGridVales;
            //if (integerDataTable.Rows.Count < 1)
            if (SearchForDuplicateProps(integerDataTable, cbInt32Props.SelectedIndex.ToString()))
            {
                MessageBox.Show("Property Already Exits");
            }
            else
            {
                string[] description = cbInt32Props.Text.Split(" ");

                // var data = new DataGridValues { Property = cbInt32Props.SelectedIndex.ToString(), Value = tbValue.Text, Description = description[1] };

                DataRow dr = integerDataTable.NewRow();
                dr[0] = cbInt32Props.SelectedIndex.ToString();
                dr[1] = tbValue.Text;
                dr[2] = description[1];

                integerDataTable.Rows.Add(dr);
            }

        }
        private static bool SearchForDuplicateProps(DataTable tempTable,string searchProp)
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


        //public void MyDictionary()
        //{
        //    FoundItems = new Dictionary<int, FoundItem>
        //    {
        //        { 1, new FoundItem() { RecordID = 1, RecordName = "Test Name 1" } },
        //        { 2, new FoundItem() { RecordID = 2, RecordName = "Test Name 2" } },
        //        { 3, new FoundItem() { RecordID = 3, RecordName = "Test Name 3" } },
        //        { 4, new FoundItem() { RecordID = 4, RecordName = "Test Name 4" } }

        //    };



        //}

        private void btnInt32Remove_Click(object sender, RoutedEventArgs e)
        {
            var index = dgInt32.SelectedIndex;
            DataGridRow currentRowIndex = dgInt32.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
            DataRow dr = integerDataTable.Rows[currentRowIndex.GetIndex()];
            dr.Delete();
            
            integerDataTable.AcceptChanges();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CreateIntegerLists("Don't need");
        }

        public void CreateIntegerLists(string filepath)
        {
            filepath = "Int32Properties.txt";

            List<string> integerLists = new List<string>();
            foreach (string line in File.ReadLines(filepath))
            {
                integerLists.Add(line);
            }
            cbInt32Props.ItemsSource = integerLists;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
        Regex regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
        }

    }
}
