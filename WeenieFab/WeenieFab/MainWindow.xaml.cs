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
        public static DataTable iidDataTable = new DataTable();
        public static DataTable positionsDataTable = new DataTable();

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

            Globals.FileChanged = false;

            btnGenerateBodyTable.Visibility = Visibility.Hidden;
            rtbBodyParts.Visibility = Visibility.Hidden;

            // For getting filename
            string[] args = Environment.GetCommandLineArgs();
            string fileToOpen = "";

            for (int i = 1; i <= args.Length -1; i++)
            {
                fileToOpen += args[i].ToString() + " ";
            }
            // Open File if one is dragged or specified.
            if (fileToOpen.Contains(".sql"))
            {
                OpenSqlFile(fileToOpen);
            }

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

            List<string> InstanceTypes = new List<string>();
            foreach (string line in File.ReadLines(@"TypeLists\InstanceIDTypes.txt"))
            {
                InstanceTypes.Add(line);
            }
            cbIidProps.ItemsSource = InstanceTypes;
            cbIidProps.SelectedIndex = 1;
            
            List<string> PositionTypes = new List<string>();
            foreach (string line in File.ReadLines(@"TypeLists\PositionTypes.txt"))
            {
                PositionTypes.Add(line);
            }
            cbPosition.ItemsSource = PositionTypes;
            cbPosition.SelectedIndex = 1;

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
        // Texbox Validations
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
        // Data Table Sorting
        public static DataTable ResortDataTable(DataTable dt, string colName, string direction)
        {
            dt.DefaultView.Sort = colName + " " + direction;
            dt = dt.DefaultView.ToTable();
            return dt;
        }
        // Text to Numeric Converters
        public static int ConvertToInteger(string text)
        {
            int i = 0;
            Int32.TryParse(text, out i);
            return i;
        }

        public static long ConvertToLong(string text)
        {
            long i = 0;
            Int64.TryParse(text, out i);
            return i;
        }


        public static uint ConvertToUInteger(string text)
        {
            uint i = 0;
            i = Convert.ToUInt32(text, 32);            
            return i;
        }
        public static float ConvertToFloat(string text)
        {
            float i = 0f;
            float.TryParse(text, out i);
            return i;
        }
        public static decimal ConvertToDecimal(string text)
        {
            decimal i = 0;
            decimal.TryParse(text, out i);
            return i;
        }
        public static int ConvertHexToDecimal(string hexValue)
        {
            //decimal i = 0;
            //decimal.TryParse(text, out i);
            //return i;
            int i = 0;
            //int i = int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);
            Int32.TryParse(hexValue, out i);

            try
            {
                i = int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);
            }
            catch (Exception)
            {

                throw;
            }

            return i;
        }
        public static string ConvertToHex(string value)
        {

            int i = ConvertToInteger(value);
            string hexValue = i.ToString("X8");

            return hexValue;

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
            iidDataTable.Clear();
            spellDataTable.Clear();
            attributeDataTable.Clear();
            attribute2DataTable.Clear();
            skillsDataTable.Clear();
            createListDataTable.Clear();
            bodypartsDataTable.Clear();
            bookInfoDataTable.Clear();
            bookPagesDataTable.Clear();
            positionsDataTable.Clear();
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

            // IID
            cbIidProps.SelectedIndex = 1;
            tbiidValue.Text = "";

            // Positions
            cbPosition.SelectedIndex = 1;
            tbPositionLoc.Text = "";
            tbCellID.Text = "";
            tbOriginX.Text = "";
            tbOriginY.Text = "";
            tbOriginZ.Text = "";
            tbAngleW.Text = "";
            tbAngleX.Text = "";
            tbAngleY.Text = "";
            tbAngleZ.Text = "";

            // Rich Text Boxes
            rtbEmoteScript.Document.Blocks.Clear();
            rtbBodyParts.Document.Blocks.Clear();

            // Generator Tab
            tbGenerator.Text = "";

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

            
            txtblockVersion.Text = "Version " + version;
        }

        // **Auto Calcs for Health/Stam/Mana and Skills**
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
        public void FileChanged()
        {
            Globals.FileChanged = true;

            txtBlockFileStatus.Text = "File has been changed, please save changes.";

        }
        public void FileChangedCheck()
        {

            MessageBoxButton buttons = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Question;
            MessageBoxResult result = MessageBox.Show("Save current Weenie?", "Possible Unsaved Changes", buttons, icon);
            if (result == MessageBoxResult.Yes)
            {
                SaveFile();
            }
            else if (result == MessageBoxResult.No)
            {

            }
        }

        // Not used currently (button is hidden) but have ideas for this.   Reuse  for importing body tables.
        private void btnGenerateBodyTable_Click(object sender, RoutedEventArgs e)
        {
            string header = $"INSERT INTO `weenie_properties_body_part` (`object_Id`, `key`, `d_Type`, `d_Val`, `d_Var`, `base_Armor`, `armor_Vs_Slash`, `armor_Vs_Pierce`, `armor_Vs_Bludgeon`, `armor_Vs_Cold`, `armor_Vs_Fire`, `armor_Vs_Acid`, `armor_Vs_Electric`, `armor_Vs_Nether`, `b_h`, `h_l_f`, `m_l_f`, `l_l_f`, `h_r_f`, `m_r_f`, `l_r_f`, `h_l_b`, `m_l_b`, `l_l_b`, `h_r_b`, `m_r_b`, `l_r_b`)";

            string bodyparts = TableToSql.ConvertBodyPart(bodypartsDataTable, tbWCID.Text, header);
            rtbBodyParts.Document.Blocks.Clear();
            rtbBodyParts.Document.Blocks.Add(new System.Windows.Documents.Paragraph(new Run(bodyparts)));
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FileChangedCheck();

        }
    }

}
