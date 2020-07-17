using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
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
using WeenieFab.Properties;
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
        public static DataTable bodypartsDataTable = new DataTable();
        public static DataTable bookInfoDataTable = new DataTable();
        public static DataTable bookPagesDataTable = new DataTable();

        public MainWindow()
        {
            InitializeComponent();
            CreateWeenieTypeList();
            CreateComboBoxLists();
            CreateDataTable();
            ClearAllDataTables();
            ClearAllFields();
            MiscSettings();

            CreateSpellList();
            GetVersion();

            btnGenerateBodyTable.Visibility = Visibility.Hidden;
            rtbBodyParts.Visibility = Visibility.Hidden;
            tbCreateItemsDestType.Visibility = Visibility.Hidden;
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
                {
                    found = true;
                    return found;
                }
                else
                    found = false;
            }

            return found;
        }

        //internal class FoundItem
        //{
        //    public int RecordID { get; set; }
        //    public string RecordName { get; set; }
        //}

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
            DataColumn shadeCreateList = new DataColumn("DropPercent");
            DataColumn tryToBondCreateList = new DataColumn("TryToBond");
            DataColumn descriptionCreateList = new DataColumn("Description");

            destTypeCreateList.DataType = Type.GetType("System.Int32");
            wcidCreateList.DataType = Type.GetType("System.Int32");
            stackSizeCreateList.DataType = Type.GetType("System.Int32");
            paletteCreateList.DataType = Type.GetType("System.Int32");
            shadeCreateList.DataType = Type.GetType("System.Single");
            tryToBondCreateList.DataType = Type.GetType("System.Boolean");

            createListDataTable.Columns.Add(destTypeCreateList);
            createListDataTable.Columns.Add(wcidCreateList);
            createListDataTable.Columns.Add(stackSizeCreateList);
            createListDataTable.Columns.Add(paletteCreateList);
            createListDataTable.Columns.Add(shadeCreateList);
            createListDataTable.Columns.Add(tryToBondCreateList);
            createListDataTable.Columns.Add(descriptionCreateList);
            dgCreateItems.DataContext = createListDataTable;

            // Body Parts
            DataColumn bodyPart = new DataColumn("BodyPart");
            DataColumn damageType = new DataColumn("DamageType");
            DataColumn damageValue = new DataColumn("Damage");
            DataColumn damageVariance = new DataColumn("DamageVariance");
            DataColumn armorLevel = new DataColumn("ArmorLevel");
            DataColumn baseHeight = new DataColumn("Height");
            
            DataColumn hlfQuad = new DataColumn("HLF");
            DataColumn mlfQuad = new DataColumn("MLF");
            DataColumn llfQuad = new DataColumn("LLF");

            DataColumn hrfQuad = new DataColumn("HRF");
            DataColumn mrfQuad = new DataColumn("MRF");
            DataColumn lrfQuad = new DataColumn("LRF");

            DataColumn hlbQuad = new DataColumn("HLB");
            DataColumn mlbQuad = new DataColumn("MLB");
            DataColumn llbQuad = new DataColumn("LLB");

            DataColumn hrbQuad = new DataColumn("HRB");
            DataColumn mrbQuad = new DataColumn("MRB");
            DataColumn lrbQuad = new DataColumn("LRB");

            DataColumn descriptionBodyParts = new DataColumn("Description");

            bodyPart.DataType = Type.GetType("System.Int32");
            damageType.DataType = Type.GetType("System.Int32");
            damageValue.DataType = Type.GetType("System.Int32");
            damageVariance.DataType = Type.GetType("System.Single");
            armorLevel.DataType = Type.GetType("System.Int32");
            baseHeight.DataType = Type.GetType("System.Int32");

            hlfQuad.DataType = Type.GetType("System.Single");
            mlfQuad.DataType = Type.GetType("System.Single");
            llfQuad.DataType = Type.GetType("System.Single");

            hrfQuad.DataType = Type.GetType("System.Single");
            mrfQuad.DataType = Type.GetType("System.Single");
            lrfQuad.DataType = Type.GetType("System.Single");

            hlbQuad.DataType = Type.GetType("System.Single");
            mlbQuad.DataType = Type.GetType("System.Single");
            llbQuad.DataType = Type.GetType("System.Single");

            hrbQuad.DataType = Type.GetType("System.Single");
            mrbQuad.DataType = Type.GetType("System.Single");
            lrbQuad.DataType = Type.GetType("System.Single");

            bodypartsDataTable.Columns.Add(bodyPart);
            bodypartsDataTable.Columns.Add(damageType);
            bodypartsDataTable.Columns.Add(damageValue);
            bodypartsDataTable.Columns.Add(damageVariance);
            bodypartsDataTable.Columns.Add(armorLevel);
            bodypartsDataTable.Columns.Add(baseHeight);

            bodypartsDataTable.Columns.Add(hlfQuad);
            bodypartsDataTable.Columns.Add(mlfQuad);
            bodypartsDataTable.Columns.Add(llfQuad);

            bodypartsDataTable.Columns.Add(hrfQuad);
            bodypartsDataTable.Columns.Add(mrfQuad);
            bodypartsDataTable.Columns.Add(lrfQuad);

            bodypartsDataTable.Columns.Add(hlbQuad);
            bodypartsDataTable.Columns.Add(mlbQuad);
            bodypartsDataTable.Columns.Add(llbQuad);

            bodypartsDataTable.Columns.Add(hrbQuad);
            bodypartsDataTable.Columns.Add(mrbQuad);
            bodypartsDataTable.Columns.Add(lrbQuad);

            bodypartsDataTable.Columns.Add(descriptionBodyParts);
            dgBodyParts.DataContext = bodypartsDataTable;

            // Book Properties
            DataColumn maxPages = new DataColumn("MaxPages");
            DataColumn maxCharsPage = new DataColumn("MaxCharsPage");

            maxPages.DataType = Type.GetType("System.Int32");
            maxCharsPage.DataType = Type.GetType("System.Int32");

            bookInfoDataTable.Columns.Add(maxPages);
            bookInfoDataTable.Columns.Add(maxCharsPage);

            dgBookInfo.DataContext = bookInfoDataTable;

            // Book Pages
            DataColumn pageIdBook = new DataColumn("PageID");
            DataColumn authorIdBook = new DataColumn("AuthorID");
            DataColumn authorNameBook = new DataColumn("AuthorName");
            DataColumn authorAccountBook = new DataColumn("AuthorAccount");
            DataColumn ignoreAuthorBook = new DataColumn("IgnoreAuthor");
            DataColumn pageTextBook = new DataColumn("PageText");

            pageIdBook.DataType = Type.GetType("System.Int32");
            // authorIdBook.DataType = Type.GetType("System.Single");
            ignoreAuthorBook.DataType = Type.GetType("System.Boolean");

            bookPagesDataTable.Columns.Add(pageIdBook);
            bookPagesDataTable.Columns.Add(authorIdBook);
            bookPagesDataTable.Columns.Add(authorNameBook);
            bookPagesDataTable.Columns.Add(authorAccountBook);
            bookPagesDataTable.Columns.Add(ignoreAuthorBook);
            bookPagesDataTable.Columns.Add(pageTextBook);
            
            dgBookPages.DataContext = bookPagesDataTable;

            // Generator


            // Positions

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

            List<string> BodyParts = new List<string>();
            foreach (string line in File.ReadLines(@"TypeLists\BodyParts.txt"))
            {
                BodyParts.Add(line);
            }
            cbBodyPart.ItemsSource = BodyParts;
            cbBodyPart.SelectedIndex = 0;

            List<string> DamageTypes = new List<string>();
            foreach (string line in File.ReadLines(@"TypeLists\DamageTypes.txt"))
            {
                DamageTypes.Add(line);
            }
            cbBodyPartDamageType.ItemsSource = DamageTypes;
            cbBodyPartDamageType.SelectedIndex = 1;

            List<string> DestinationTypes = new List<string>();
            foreach (string line in File.ReadLines(@"TypeLists\DestinationTypes.txt"))
            {
                DestinationTypes.Add(line);
            }
            cbDestinationType.ItemsSource = DestinationTypes;
            cbDestinationType.SelectedIndex = 1;

        }
        

        // Testing Search
        public void CreateSpellList()
            {
                List<SpellNames> listSpellNames = new List<SpellNames>();

                foreach (string line in File.ReadLines(@"TypeLists\SpellNames.txt"))
                {
                    string[] spellData = line.Split(",");

                    listSpellNames.Add(new SpellNames { SpellID = ConvertToInteger(spellData[0]), SpellName = spellData[1] });
                }
                lvSpellsList.ItemsSource = listSpellNames;
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvSpellsList.ItemsSource);
                view.Filter = SpellFilter;
            }
            private bool SpellFilter(object spellname)
            {
                if (String.IsNullOrEmpty(tbSpellSearch.Text))
                    return true;
                else
                    return ((spellname as SpellNames).SpellName.IndexOf(tbSpellSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
            {
                CollectionViewSource.GetDefaultView(lvSpellsList.ItemsSource).Refresh();
            }

            public void CreateWeenieTypeList()
            {

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
            public static uint ConvertToUInteger(string text)
            {
                uint i = 0;
                i = Convert.ToUInt32(text, 32);
                //Convert.ToUInt64(text,)
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
                createListDataTable.Clear();
                bodypartsDataTable.Clear();
                bookInfoDataTable.Clear();
                bookPagesDataTable.Clear();
            }
            public void ResetIndexAllDataGrids()
            {
                dgInt32.SelectedIndex = -1;

            }
            public void ClearAllFields()
            {

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
                tbCreateItemsDescription.Text = "";
                tbCreateItemsDestType.Text = "";
                tbCreateItemsDropRate.Text = "";
                tbCreateItemsPalette.Text = "";
                tbCreateItemsStackSize.Text = "";
                tbCreateItemsWCID.Text = "";

                tbBodyPartDamageValue.Text = "";
                tbBodyPartDamageVariance.Text = "";
                tbBodyPartArmorLevel.Text = "";
                tbBodyPartBase_Height.Text = "";

                tbBodyPartQuadHighLF.Text = "";
                tbBodyPartQuadMiddleLF.Text = "";
                tbBodyPartQuadLowLF.Text = "";

                tbBodyPartQuadHighRF.Text = "";
                tbBodyPartQuadMiddleRF.Text = "";
                tbBodyPartQuadLowRF.Text = "";

                tbBodyPartQuadHighLB.Text = "";
                tbBodyPartQuadMiddleLB.Text = "";
                tbBodyPartQuadLowLB.Text = "";

                tbBodyPartQuadHighRB.Text = "";
                tbBodyPartQuadMiddleRB.Text = "";
                tbBodyPartQuadLowRB.Text = "";

                // Books
                tbMaxPages.Text = "";
                tbMaxChars.Text = "";

                tbPageID.Text = "";
                tbAuthorName.Text = "";
                tbPageText.Text = "";
                rdbBookFalse.IsChecked = true;

                // Rich Text Boxes
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
                cbBodyPart.SelectedIndex = 0;
                cbBodyPartDamageType.SelectedIndex = 1;
                cbDestinationType.SelectedIndex = 1;


                ClearAttributeFields();
                ClearAttribute2Fields();

            }
            public void MiscSettings()
            {
                rtbEmoteScript.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
                rtbEmoteScript.Document.PageWidth = 2000;
                if (WeenieFabUser.Default.AutoCalcHealth == true)
                    chkbAutoHealth.IsChecked = true;


            }

            private void lvSpellsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                // Don't think I need this anymore.
            }
            private void GetVersion()
            {

                System.Reflection.Assembly executingAssembly = System.Reflection.Assembly.GetExecutingAssembly();
                var fileVersionInfo = FileVersionInfo.GetVersionInfo(executingAssembly.Location);
                var version = fileVersionInfo.FileVersion;

                lblVersion.Content = "Version " + version;
            }

            private void chkbAutoHealth_Changed(object sender, RoutedEventArgs e)
            {
                if (chkbAutoHealth.IsChecked == true)
                {
                    WeenieFabUser.Default.AutoCalcHealth = true;
                    WeenieFabUser.Default.Save();
                }
                else
                {
                    WeenieFabUser.Default.AutoCalcHealth = false;
                    WeenieFabUser.Default.Save();
                }
            }

            private void tbHealthCurrentLevel_TextChanged(object sender, TextChangedEventArgs e)
            {
                if (WeenieFabUser.Default.AutoCalcHealth == true)
                {
                    int attribEndurance = ConvertToInteger(tbAttribEndurance.Text) / 2;
                    int finalHealth = ConvertToInteger(tbHealthCurrentLevel.Text);
                    tbHealthInitLevel.Text = (finalHealth - attribEndurance).ToString();
                }          
            }

            private void tbStaminaCurrentLevel_TextChanged(object sender, TextChangedEventArgs e)
            {
                if (WeenieFabUser.Default.AutoCalcHealth == true)
                {
                    int attribEndurance = ConvertToInteger(tbAttribEndurance.Text);
                    int finalStamina = ConvertToInteger(tbStaminaCurrentLevel.Text);
                    tbStaminaInitLevel.Text = (finalStamina - attribEndurance).ToString();
                }
            }

            private void tbManaCurrentLevel_TextChanged(object sender, TextChangedEventArgs e)
            {
                if (WeenieFabUser.Default.AutoCalcHealth == true)
                {
                    int attribSelf = ConvertToInteger(tbAttribSelf.Text);
                    int finalMana = ConvertToInteger(tbManaCurrentLevel.Text);
                    tbManaInitLevel.Text = (finalMana - attribSelf).ToString();
                }
            }

            private void chkbSkillCalc_Changed(object sender, RoutedEventArgs e)
            {
                if (chkbAutoHealth.IsChecked == true)
                {
                    WeenieFabUser.Default.AutoCalcSkill = true;
                    WeenieFabUser.Default.Save();
                }
                else
                {
                    WeenieFabUser.Default.AutoCalcSkill = false;
                    WeenieFabUser.Default.Save();
                
                }
            }

            private void tbSkillFinalLevel_TextChanged(object sender, TextChangedEventArgs e)
            {
                AutoSkillCalc();
            }
            private void cbSkillType_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                // Not sure if I want to do this or not.  Will leave out for now, unless it is asked for. HQ 7/8
                // AutoSkillCalc();
            }
            public void AutoSkillCalc()
            {
                // Skill Formulas based on Attribs

                int strength = ConvertToInteger(tbAttribStrength.Text);
                int endur = ConvertToInteger(tbAttribEndurance.Text);
                int coord = ConvertToInteger(tbAttribCoordination.Text);
                int quick = ConvertToInteger(tbAttribQuickness.Text);
                int focus = ConvertToInteger(tbAttribFocus.Text);
                int self = ConvertToInteger(tbAttribSelf.Text);


                if (WeenieFabUser.Default.AutoCalcSkill == true)
                {
                    switch (cbSkillType.SelectedIndex)
                    {

                        case 6:  // MeleeD
                        case 46: // Finesse Weapons
                        case 51: // Sneak Attack
                            tbSkillLevel.Text = (ConvertToInteger(tbSkillFinalLevel.Text) - ((quick + coord) / 3)).ToString();
                            break;
                        case 7:  // MissileD
                            tbSkillLevel.Text = (ConvertToInteger(tbSkillFinalLevel.Text) - ((quick + coord) / 5)).ToString();
                            break;
                        case 14:  // Arcane Lore
                            tbSkillLevel.Text = (ConvertToInteger(tbSkillFinalLevel.Text) - (focus / 3)).ToString();
                            break;
                        case 15:  // Magic D
                            tbSkillLevel.Text = (ConvertToInteger(tbSkillFinalLevel.Text) - ((focus + self) / 7)).ToString();
                            break; 
                        case 16:  // Mana C
                            tbSkillLevel.Text = (ConvertToInteger(tbSkillFinalLevel.Text) - ((focus + self) / 6)).ToString();
                            break;
                        case 18:  // Item Appraisal - Item Tink
                            tbSkillLevel.Text = (ConvertToInteger(tbSkillFinalLevel.Text) - ((focus + coord) / 2)).ToString();
                            break;
                        case 19:  // Personal Appraisal - Assess Persoon
                        case 20:  // Deception
                        case 27:  // Creature Appraisal - Asses Creature
                        case 35:  // Leadership
                        case 36:  // Loyalty
                        case 40:  // Salvaging
                            tbSkillLevel.Text = (ConvertToInteger(tbSkillFinalLevel.Text)).ToString();
                            break;
                        case 21:  // Healing
                        case 23:  // Lockpick
                        case 37:  // Fletching
                        case 38:  // Alchemy
                        case 39:  // Cooking
                            tbSkillLevel.Text = (ConvertToInteger(tbSkillFinalLevel.Text) - ((focus + coord) / 3)).ToString();
                            break;
                        case 22:  // Jump
                        case 48:  // Shield
                            tbSkillLevel.Text = (ConvertToInteger(tbSkillFinalLevel.Text) - ((strength + coord) / 2)).ToString();
                            break;
                        case 24:  // Run
                            tbSkillLevel.Text = (ConvertToInteger(tbSkillFinalLevel.Text) - quick).ToString();
                            break;
                        case 28:  // Weapon Appraisal - Weapon Tink
                            tbSkillLevel.Text = (ConvertToInteger(tbSkillFinalLevel.Text) - ((focus + strength) / 2)).ToString();
                            break;
                        case 29:  // Armor Appraisal - Armor Tink
                            tbSkillLevel.Text = (ConvertToInteger(tbSkillFinalLevel.Text) - ((focus + endur) / 2)).ToString();
                            break;
                        case 30:  // Magic Item Appraisal - Magic Item Tink
                            tbSkillLevel.Text = (ConvertToInteger(tbSkillFinalLevel.Text) - focus).ToString();
                            break;
                        case 31:  // Creature Magic
                        case 32:  // Item Magic
                        case 33:  // Life Magic
                        case 34:  // War Magic
                        case 43:  // Void Magic
                            tbSkillLevel.Text = (ConvertToInteger(tbSkillFinalLevel.Text) - ((focus + self) / 4)).ToString();
                            break;
                        case 41:  // Two Hand
                        case 44:  // Heavy Weapons
                        case 45:  // Light Weapons
                        case 52:  // Dirty Fighting
                            tbSkillLevel.Text = (ConvertToInteger(tbSkillFinalLevel.Text) - ((strength + coord) / 3)).ToString();
                            break;
                        case 47:  // Missile Weapons
                            tbSkillLevel.Text = (ConvertToInteger(tbSkillFinalLevel.Text) - coord / 2).ToString();
                            break;
                        case 49:  // Dual Wield
                            tbSkillLevel.Text = (ConvertToInteger(tbSkillFinalLevel.Text) - ((coord * 2) / 3)).ToString();
                            break;
                        case 50:  // Recklessness
                            tbSkillLevel.Text = (ConvertToInteger(tbSkillFinalLevel.Text) - ((strength + quick) / 3)).ToString();
                            break;
                        case 54:  // Summoning
                            tbSkillLevel.Text = (ConvertToInteger(tbSkillFinalLevel.Text) - ((endur + self) / 3)).ToString();
                            break;
                        // Ignored (Unused)
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 8:
                        case 9:
                        case 10:
                        case 11:
                        case 12:
                        case 13:
                        case 17:
                        case 25:
                        case 26:
                        case 42:
                        case 53:
                        default:
                            break;
                    }
                }

            }

            private void btnGenerateBodyTable_Click(object sender, RoutedEventArgs e)
            {
                string header = $"INSERT INTO `weenie_properties_body_part` (`object_Id`, `key`, `d_Type`, `d_Val`, `d_Var`, `base_Armor`, `armor_Vs_Slash`, `armor_Vs_Pierce`, `armor_Vs_Bludgeon`, `armor_Vs_Cold`, `armor_Vs_Fire`, `armor_Vs_Acid`, `armor_Vs_Electric`, `armor_Vs_Nether`, `b_h`, `h_l_f`, `m_l_f`, `l_l_f`, `h_r_f`, `m_r_f`, `l_r_f`, `h_l_b`, `m_l_b`, `l_l_b`, `h_r_b`, `m_r_b`, `l_r_b`)";

                string bodyparts = TableToSql.ConvertBodyPart(bodypartsDataTable, tbWCID.Text, header);
                rtbBodyParts.Document.Blocks.Clear();
                rtbBodyParts.Document.Blocks.Add(new System.Windows.Documents.Paragraph(new Run(bodyparts)));
            }


        }

    }
