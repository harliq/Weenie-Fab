using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
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
using Path = System.IO.Path;

namespace WeenieFab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // public int SelectedItemID { get; set; }
        public static DataTable integerDataTable = new DataTable();
        public static DataTable integer64DataTable = new DataTable();
        public static DataTable boolDataTable = new DataTable();
        public static DataTable floatDataTable = new DataTable();
        public static DataTable stringDataTable = new DataTable();
        public static DataTable didDataTable = new DataTable();
        public static DataTable spellDataTable = new DataTable();

        public MainWindow()
        {
            InitializeComponent();
            CreateWeenieTypeList();
            CreateComboBoxLists();
            CreateDataTable();


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
            dgInt32.DataContext = integerDataTable;

            integer64DataTable.Columns.Add(new DataColumn("Property"));
            integer64DataTable.Columns.Add(new DataColumn("Value"));
            integer64DataTable.Columns.Add(new DataColumn("Description"));
            dgInt64.DataContext = integer64DataTable;

            boolDataTable.Columns.Add(new DataColumn("Property"));
            boolDataTable.Columns.Add(new DataColumn("Value"));
            boolDataTable.Columns.Add(new DataColumn("Description"));
            dgBool.DataContext = boolDataTable;

            floatDataTable.Columns.Add(new DataColumn("Property"));
            floatDataTable.Columns.Add(new DataColumn("Value"));
            floatDataTable.Columns.Add(new DataColumn("Description"));
            dgFloat.DataContext = floatDataTable;

            stringDataTable.Columns.Add(new DataColumn("Property"));
            stringDataTable.Columns.Add(new DataColumn("Value"));
            stringDataTable.Columns.Add(new DataColumn("Description"));
            dgString.DataContext = stringDataTable;

            didDataTable.Columns.Add(new DataColumn("Property"));
            didDataTable.Columns.Add(new DataColumn("Value"));
            didDataTable.Columns.Add(new DataColumn("Description"));
            dgDiD.DataContext = didDataTable;

            spellDataTable.Columns.Add(new DataColumn("Property"));
            spellDataTable.Columns.Add(new DataColumn("Value"));
            spellDataTable.Columns.Add(new DataColumn("Description"));
            dgSpell.DataContext = spellDataTable;

        }

        public void CreateComboBoxLists()
        {
            //string filepaths = "Int32Types.txt," +
            //                   "Int64Types.txt," +
            //                   "BoolTypes.txt," +
            //                   "FloatTypes.txt" +
            //                   "StringTypes.txt" +
            //                   "DiDTypes.txt";

            //string[] filepath = filepaths.Split(",");

            //for (int i = 0; i < filepath.Length; i++)
            //{
            //    filepath[i] ;

            //}

           // string filepath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            List<string> integer32List = new List<string>();
            foreach (string line in File.ReadLines(@"TypeLists\Int32Types.txt"))
            {
                integer32List.Add(line);
            }
            cbInt32Props.ItemsSource = integer32List;
            cbInt32Props.SelectedIndex = 1;

            List<string> integer64List = new List<string>();
            foreach (string line in File.ReadLines(@"TypeLists\Int64Types.txt"))
            {
                integer64List.Add(line);
            }
            cbInt64Props.ItemsSource = integer64List;
            cbInt64Props.SelectedIndex = 1;

            List<string> BoolList = new List<string>();
            foreach (string line in File.ReadLines(@"TypeLists\BoolTypes.txt"))
            {
                BoolList.Add(line);
            }
            cbBoolProps.ItemsSource = BoolList;
            cbBoolProps.SelectedIndex = 1;

            List<string> FloatList = new List<string>();
            foreach (string line in File.ReadLines(@"TypeLists\FloatTypes.txt"))
            {
                FloatList.Add(line);
            }
            cbFloatProps.ItemsSource = FloatList;
            cbFloatProps.SelectedIndex = 1;

            List<string> StringList = new List<string>();
            foreach (string line in File.ReadLines(@"TypeLists\StringTypes.txt"))
            {
                StringList.Add(line);
            }
            cbStringProps.ItemsSource = StringList;
            cbStringProps.SelectedIndex = 1;

            List<string> DiDList = new List<string>();
            foreach (string line in File.ReadLines(@"TypeLists\DiDTypes.txt"))
            {
                DiDList.Add(line);
            }
            cbDiDProps.ItemsSource = DiDList;
            cbDiDProps.SelectedIndex = 1;

        }

        public void CreateWeenieTypeList()
        {
            // string filepath = "WeenieTypes.txt";
            // string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"TypeLists\WeenieTypes.txt");
            string filepath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"TypeLists\WeenieTypes.txt");


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

        // Toolbar Buttons
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
