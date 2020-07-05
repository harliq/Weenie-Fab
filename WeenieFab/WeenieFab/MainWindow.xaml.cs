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
        public static DataTable attributeDataTable = new DataTable();
        public static DataTable attribute2DataTable = new DataTable();
        public static DataTable skillsDataTable = new DataTable();
        public static DataTable createListDataTable = new DataTable();


        public MainWindow()
        {
            InitializeComponent();
            CreateWeenieTypeList();
            CreateComboBoxLists();
            CreateDataTable();
            ClearAllDataTables();
            ClearAllDataGrids();
            MiscSettings();
        }

        private static bool SearchForDuplicateProps(DataTable tempTable, int searchProp)
        {
            bool found = false;

            foreach (DataRow row in tempTable.Rows)
            {
                if (row[0].Equals(searchProp))
                    found = true;
                else
                    found = false;
            }

            return found;
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
            DataColumn propertyInt = new DataColumn("Property");
            DataColumn propertyInt64 = new DataColumn("Property");
            DataColumn propertyFloat = new DataColumn("Property");
            DataColumn propertyBool = new DataColumn("Property");
            DataColumn propertyString = new DataColumn("Property");
            DataColumn propertyDiD = new DataColumn("Property");

            propertyInt.DataType = Type.GetType("System.Int32");
            propertyInt64.DataType = Type.GetType("System.Int32");
            propertyFloat.DataType = Type.GetType("System.Int32");
            propertyBool.DataType = Type.GetType("System.Int32");
            propertyString.DataType = Type.GetType("System.Int32");
            propertyDiD.DataType = Type.GetType("System.Int32");


            DataColumn valueInt = new DataColumn("Value");
            DataColumn valueInt64 = new DataColumn("Value");
            DataColumn valueFloat = new DataColumn("Value");
            DataColumn valueBool = new DataColumn("Value");
            DataColumn valueString = new DataColumn("Value");
            DataColumn valueDiD = new DataColumn("Value");

            valueInt.DataType = Type.GetType("System.Int32");
            valueInt64.DataType = Type.GetType("System.Int32");
            valueFloat.DataType = Type.GetType("System.Single");
            valueBool.DataType = Type.GetType("System.Boolean");
            valueString.DataType = Type.GetType("System.String");
            valueDiD.DataType = Type.GetType("System.String");

            DataColumn descriptionInt = new DataColumn("Description");
            DataColumn descriptionInt64 = new DataColumn("Description");
            DataColumn descriptionFloat = new DataColumn("Description");
            DataColumn descriptionBool = new DataColumn("Description");
            DataColumn descriptionString = new DataColumn("Description");
            DataColumn descriptionDiD = new DataColumn("Description");
            DataColumn descriptionSpellBook = new DataColumn("Description");

            // Int32
            integerDataTable.Columns.Add(propertyInt);
            integerDataTable.Columns.Add(valueInt);
            integerDataTable.Columns.Add(descriptionInt);
            dgInt32.DataContext = integerDataTable.DefaultView;

            // Int64
            integer64DataTable.Columns.Add(propertyInt64);
            integer64DataTable.Columns.Add(valueInt64);
            integer64DataTable.Columns.Add(descriptionInt64);
            dgInt64.DataContext = integer64DataTable.DefaultView;

            // Bool Table
            boolDataTable.Columns.Add(propertyBool);
            boolDataTable.Columns.Add(valueBool);
            boolDataTable.Columns.Add(descriptionBool);
            dgBool.DataContext = boolDataTable;

            // Float Table
            floatDataTable.Columns.Add(propertyFloat);
            floatDataTable.Columns.Add(valueFloat);
            floatDataTable.Columns.Add(descriptionFloat);
            dgFloat.DataContext = floatDataTable;

            // DiD
            didDataTable.Columns.Add(propertyDiD);
            didDataTable.Columns.Add(valueDiD);
            didDataTable.Columns.Add(descriptionDiD);
            dgDiD.DataContext = didDataTable;

            // Strings Table
            stringDataTable.Columns.Add(propertyString);
            stringDataTable.Columns.Add(valueString);
            stringDataTable.Columns.Add(descriptionString);
            dgString.DataContext = stringDataTable;

            //SpellBook
            DataColumn spellIdInt = new DataColumn("Property");
            DataColumn probabilityFloat = new DataColumn("Value");
            spellIdInt.DataType = Type.GetType("System.Int32");
            probabilityFloat.DataType = Type.GetType("System.Single");

            spellDataTable.Columns.Add(spellIdInt);
            spellDataTable.Columns.Add(probabilityFloat);
            spellDataTable.Columns.Add(descriptionSpellBook);
            dgSpell.DataContext = spellDataTable;

            //Attributes
            DataColumn typeAttrib = new DataColumn("Type");
            DataColumn initLevelAttrib = new DataColumn("InitLevel");
            DataColumn levelCPAttrib = new DataColumn("LevelCP");
            DataColumn cpSpentAttrib = new DataColumn("CPSpent");
            DataColumn descriptionAttrib = new DataColumn("Description");
            typeAttrib.DataType = Type.GetType("System.Int32");
            initLevelAttrib.DataType = Type.GetType("System.Single");
            levelCPAttrib.DataType = Type.GetType("System.Single");
            cpSpentAttrib.DataType = Type.GetType("System.Single");
            attributeDataTable.Columns.Add(typeAttrib);
            attributeDataTable.Columns.Add(initLevelAttrib);
            attributeDataTable.Columns.Add(levelCPAttrib);
            attributeDataTable.Columns.Add(cpSpentAttrib);
            attributeDataTable.Columns.Add(descriptionAttrib);
            dgAttributes.DataContext = attributeDataTable;

            //Attributes 2
            DataColumn typeAttrib2 = new DataColumn("Type");
            DataColumn initLevelAttrib2 = new DataColumn("InitLevel");
            DataColumn levelCPAttrib2 = new DataColumn("LevelCP");
            DataColumn cpSpentAttrib2 = new DataColumn("CPSpent");
            DataColumn currentLevelAttrib2 = new DataColumn("CurrentLevel");
            DataColumn descriptionAttrib2 = new DataColumn("Description");
            typeAttrib2.DataType = Type.GetType("System.Int32");
            initLevelAttrib2.DataType = Type.GetType("System.Single");
            levelCPAttrib2.DataType = Type.GetType("System.Single");
            cpSpentAttrib2.DataType = Type.GetType("System.Single");
            currentLevelAttrib2.DataType = Type.GetType("System.Single");
            attribute2DataTable.Columns.Add(typeAttrib2);
            attribute2DataTable.Columns.Add(initLevelAttrib2);
            attribute2DataTable.Columns.Add(levelCPAttrib2);
            attribute2DataTable.Columns.Add(cpSpentAttrib2);
            attribute2DataTable.Columns.Add(currentLevelAttrib2);
            attribute2DataTable.Columns.Add(descriptionAttrib2);
            dgAttributesTwo.DataContext = attribute2DataTable;

            //Skills - TODO: need to fix some of the types
            DataColumn typeSkills = new DataColumn("Type");
            DataColumn levelPPSkills = new DataColumn("LevelPP");
            DataColumn sacSkills = new DataColumn("SAC");
            DataColumn ppSkills = new DataColumn("PP");
            DataColumn initLevelSkills = new DataColumn("InitLevel");
            DataColumn resistLCSkills = new DataColumn("ResistLC");
            DataColumn lastUsedSkills = new DataColumn("LastUsed");
            DataColumn descriptionSkills = new DataColumn("Description");
            typeSkills.DataType = Type.GetType("System.Int32");
            levelPPSkills.DataType = Type.GetType("System.Single");
            sacSkills.DataType = Type.GetType("System.Single");
            ppSkills.DataType = Type.GetType("System.Single");
            initLevelSkills.DataType = Type.GetType("System.Single");
            resistLCSkills.DataType = Type.GetType("System.Single");
            lastUsedSkills.DataType = Type.GetType("System.Single");
            skillsDataTable.Columns.Add(typeSkills);
            skillsDataTable.Columns.Add(levelPPSkills);
            skillsDataTable.Columns.Add(sacSkills);
            skillsDataTable.Columns.Add(ppSkills);
            skillsDataTable.Columns.Add(initLevelSkills);
            skillsDataTable.Columns.Add(resistLCSkills);
            skillsDataTable.Columns.Add(lastUsedSkills);
            skillsDataTable.Columns.Add(descriptionSkills);
            dgSkills.DataContext = skillsDataTable;

            // Create List
            DataColumn destTypeCreateList = new DataColumn("DestinationType");
            DataColumn wcidCreateList = new DataColumn("WCID");
            DataColumn stackSizeCreateList = new DataColumn("StackSize");
            DataColumn paletteCreateList = new DataColumn("Palette");
            DataColumn shadeCreateList = new DataColumn("Shade");
            DataColumn tryToBondCreateList = new DataColumn("DropRate");
            DataColumn descriptionCreateList = new DataColumn("Description");

            destTypeCreateList.DataType = Type.GetType("System.Int32");
            wcidCreateList.DataType = Type.GetType("System.Int32");
            stackSizeCreateList.DataType = Type.GetType("System.Int32");
            paletteCreateList.DataType = Type.GetType("System.Int32");
            shadeCreateList.DataType = Type.GetType("System.Int32");
            tryToBondCreateList.DataType = Type.GetType("System.Single");

            createListDataTable.Columns.Add(destTypeCreateList);
            createListDataTable.Columns.Add(wcidCreateList);
            createListDataTable.Columns.Add(stackSizeCreateList);
            createListDataTable.Columns.Add(paletteCreateList);
            createListDataTable.Columns.Add(shadeCreateList);
            createListDataTable.Columns.Add(tryToBondCreateList);
            createListDataTable.Columns.Add(descriptionCreateList);



        }

        public void CreateComboBoxLists()
        {
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

            List<string> SkillList = new List<string>();
            foreach (string line in File.ReadLines(@"TypeLists\SkillTypes.txt"))
            {
                SkillList.Add(line);
            }
            cbSkillType.ItemsSource = SkillList;
            cbSkillType.SelectedIndex = 6;
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

        private void IntValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void FloatValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.-]+");
            e.Handled = regex.IsMatch(e.Text);
        }

 
        public static DataTable ResortDataTable(DataTable dt, string colName, string direction)
        {
            dt.DefaultView.Sort = colName + " " + direction;
            dt = dt.DefaultView.ToTable();
            return dt;
        }

        public static int ConvertToInteger(string text)
        {
            int i = 0;
            Int32.TryParse(text, out i);
            return i;
        }

        public static float ConvertToFloat(string text)
        {
            float i = 0f;
            float.TryParse(text, out i);
            return i;
        }

        // UI Stuff

        public void ClearAllDataTables()
        {
            integerDataTable.Clear();
            integer64DataTable.Clear();
            boolDataTable.Clear();
            floatDataTable.Clear();
            stringDataTable.Clear();
            didDataTable.Clear();
            spellDataTable.Clear();
            attributeDataTable.Clear();
            attribute2DataTable.Clear();
            skillsDataTable.Clear();
        }
        public void ClearAllDataGrids()
        {
            //dgInt32.Items.Clear();
            //dgInt64.Items.Clear();
            //dgBool.Items.Clear();
            //dgFloat.Items.Clear();
            //dgString.Items.Clear();
            //dgDiD.Items.Clear();
            //dgSpell.Items.Clear();
        }
        public void ResetIndexAllDataGrids()
        {
            dgInt32.SelectedIndex = -1;

            //dgInt64.Items.Clear();
            //dgBool.Items.Clear();
            //dgFloat.Items.Clear();
            //dgString.Items.Clear();
            //dgDiD.Items.Clear();
            //dgSpell.Items.Clear();
        }
        public void ClearAllFields()
        {
            // string clearContents = "";

            tbWCID.Text = "";
            tbWeenieName.Text = "";
            tbValue.Text = "";
            tb64Value.Text = "";
            tbFloatValue.Text = "";
            tbStringValue.Text = "";
            tbDiDValue.Text = "";
            tbSpellId.Text = "";
            tbSpellValue.Text = "";
            tbSkillLevel.Text = "";

            //rtbEmoteScript.Document.Blocks.Add(new System.Windows.Documents.Paragraph(new Run(clearContents)));
            //rtbBodyParts.Document.Blocks.Add(new System.Windows.Documents.Paragraph(new Run(clearContents)));

            rtbEmoteScript.Document.Blocks.Clear();
            rtbBodyParts.Document.Blocks.Clear();

            cbWeenieType.SelectedIndex = 1;
            cbInt32Props.SelectedIndex = 1;
            cbInt64Props.SelectedIndex = 1;
            cbBoolProps.SelectedIndex = 1;
            cbFloatProps.SelectedIndex = 1;
            cbStringProps.SelectedIndex = 1;
            cbDiDProps.SelectedIndex = 1;
            cbSkillType.SelectedIndex = 1;

            ClearAttributeFields();
            ClearAttribute2Fields();

        }
        public void MiscSettings()
        {
            rtbEmoteScript.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            rtbEmoteScript.Document.PageWidth = 2000;

        }


    }
}
