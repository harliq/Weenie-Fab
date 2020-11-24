using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using WeenieFab.Properties;

namespace WeenieFab
{
    /// <summary>
    /// Interaction logic for LootProfiler.xaml
    /// </summary>
    public partial class LootProfiler : Window
    {
        public LootProfiler()
        {
            InitializeComponent();
            CreateLootProfileComboBoxLists();
        }
        private void btnLootProfileClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnLootProfileOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open DeathTreasure File";
            ofd.Filter = "All Weenie Types|*.sql|SQL files|*.sql";
            ofd.InitialDirectory = WeenieFabUser.Default.DefaultSqlPath;
            Nullable<bool> result = ofd.ShowDialog();

            if (result == true)
            {
                var formData = new DeathTreasure();

                formData = OpenLootProfileFile(ofd.FileName);
                FillForm(formData);
            }
        }
        private void btnLootProfileSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save DeathTreasure File";
            sfd.Filter = "SQL files|*.sql";
            sfd.InitialDirectory = WeenieFabUser.Default.DefaultSqlPath;
            sfd.FileName = tbDeathTreasureDID.Text + $".sql";
            Nullable<bool> result = sfd.ShowDialog();
            if (result == true)
            {
                SaveDeathTreasureFile(SaveDeathTreasureForm(), sfd.FileName);
            }
        }
        private void btnClearForm_Click(object sender, RoutedEventArgs e)
        {
            tbDeathTreasureDID.Text = "";
            tbTier.Text = "";
            tbLootQualityMod.Text = "";
            tbUnknownChances.Text = "";

            tbItemChance.Text = "";
            tbItemMinAmount.Text = "";
            tbItemMaxAmount.Text = "";
            cbItemTreasureType.SelectedIndex = 0;

            tbMagicItemChance.Text = "";
            tbMagicItemMinAmount.Text = "";
            tbMagicItemMaxAmount.Text = "";
            cbMagicItemTreasureType.SelectedIndex = 0;

            tbMundaneItemChance.Text = "";
            tbMundaneItemMinAmount.Text = "";
            tbMundaneItemMaxAmount.Text = "";
            cbMundaneItemTreasureType.SelectedIndex = 0;

            tbLastModifed.Text = "";
        }
        private void btnFormDefaults_Click(object sender, RoutedEventArgs e)
        {
            tbLootQualityMod.Text = "0";
            tbUnknownChances.Text = "19";

            tbItemChance.Text = "100";
            tbItemMinAmount.Text = "1";
            tbItemMaxAmount.Text = "2";
            cbItemTreasureType.SelectedIndex = 7;

            tbMagicItemChance.Text = "100";
            tbMagicItemMinAmount.Text = "1";
            tbMagicItemMaxAmount.Text = "2";
            cbMagicItemTreasureType.SelectedIndex = 7;

            tbMundaneItemChance.Text = "100";
            tbMundaneItemMinAmount.Text = "1";
            tbMundaneItemMaxAmount.Text = "2";
            cbMundaneItemTreasureType.SelectedIndex = 6;
        }
        private void FillForm(DeathTreasure lootProfile)
        {
            tbDeathTreasureDID.Text = lootProfile.DTdid.ToString();
            tbTier.Text = lootProfile.Tier.ToString();
            tbLootQualityMod.Text = lootProfile.LootQualityMod.ToString();
            tbUnknownChances.Text = lootProfile.UnknownChances.ToString();

            tbItemChance.Text = lootProfile.ItemChance.ToString();
            tbItemMinAmount.Text = lootProfile.ItemMinAmount.ToString();
            tbItemMaxAmount.Text = lootProfile.ItemMaxAmount.ToString();
            cbItemTreasureType.SelectedIndex = lootProfile.ItemTreasureTypeSelectionChances - 1;

            tbMagicItemChance.Text = lootProfile.MagicItemChance.ToString();
            tbMagicItemMinAmount.Text = lootProfile.MagicItemMinAmount.ToString();
            tbMagicItemMaxAmount.Text = lootProfile.MagicItemMaxAmount.ToString();
            cbMagicItemTreasureType.SelectedIndex = lootProfile.MagicItemTreasureTypeSelectionChances - 1;

            tbMundaneItemChance.Text = lootProfile.MundaneItemChance.ToString();
            tbMundaneItemMinAmount.Text = lootProfile.MundaneItemMinAmount.ToString();
            tbMundaneItemMaxAmount.Text = lootProfile.MundaneItemMaxAmount.ToString();
            cbMundaneItemTreasureType.SelectedIndex = lootProfile.MundaneItemTreasureTypeSelectionChances - 1;

            tbLastModifed.Text = lootProfile.LastModified;
        }
        public static DeathTreasure OpenLootProfileFile(string filepath)
        {
            // Opens sql file
            var profileData = new DeathTreasure();

            foreach (string line in File.ReadLines(filepath))
            {
                if (line.Contains("VALUES"))
                {

                    string dtRegEx = @"VALUES\s*\((\d+),\s(\d+),\s([0-9]*\.[0-9]+|[0-9]+),\s(\d+),\s(\d+),\s(\d+),\s(\d+),\s(\d+),\s(\d+),\s(\d+),\s(\d+),\s(\d+),\s(\d+),\s(\d+),\s(\d+),\s(\d+),.*'(.*)'.*$";
                    //string linedata = "";


                    var match = Regex.Match(line, dtRegEx);

                    //string linedata = "";
                    //linedata = line.Replace("VALUES", "");
                    //linedata = linedata.Replace("(", "");
                    //linedata = linedata.Replace(");", "");
                    //linedata = linedata.Replace("'", "");
                    //string[] lootProfileValues = linedata.Split(',');

                    profileData.DTdid = MainWindow.ConvertToInteger(match.Groups[1].ToString());
                    profileData.Tier = MainWindow.ConvertToInteger(match.Groups[2].ToString());
                    profileData.LootQualityMod = MainWindow.ConvertToFloat(match.Groups[3].ToString());
                    profileData.UnknownChances = MainWindow.ConvertToInteger(match.Groups[4].ToString());
                    profileData.ItemChance = MainWindow.ConvertToInteger(match.Groups[5].ToString());
                    profileData.ItemMinAmount = MainWindow.ConvertToInteger(match.Groups[6].ToString());
                    profileData.ItemMaxAmount = MainWindow.ConvertToInteger(match.Groups[7].ToString());
                    profileData.ItemTreasureTypeSelectionChances = MainWindow.ConvertToInteger(match.Groups[8].ToString());
                    profileData.MagicItemChance = MainWindow.ConvertToInteger(match.Groups[9].ToString());
                    profileData.MagicItemMinAmount = MainWindow.ConvertToInteger(match.Groups[10].ToString());
                    profileData.MagicItemMaxAmount = MainWindow.ConvertToInteger(match.Groups[11].ToString());
                    profileData.MagicItemTreasureTypeSelectionChances = MainWindow.ConvertToInteger(match.Groups[12].ToString());
                    profileData.MundaneItemChance = MainWindow.ConvertToInteger(match.Groups[13].ToString());
                    profileData.MundaneItemMinAmount = MainWindow.ConvertToInteger(match.Groups[14].ToString());
                    profileData.MundaneItemMaxAmount = MainWindow.ConvertToInteger(match.Groups[15].ToString());
                    profileData.MundaneItemTreasureTypeSelectionChances = MainWindow.ConvertToInteger(match.Groups[16].ToString());
                    profileData.LastModified = match.Groups[17].ToString();
                }
            }
            return profileData;
        }
        public DeathTreasure SaveDeathTreasureForm()
        {
            var saveProfileData = new DeathTreasure();

            saveProfileData.DTdid = MainWindow.ConvertToInteger(tbDeathTreasureDID.Text);
            saveProfileData.Tier = MainWindow.ConvertToInteger(tbTier.Text);
            saveProfileData.LootQualityMod = float.Parse(tbLootQualityMod.Text);
            saveProfileData.UnknownChances = MainWindow.ConvertToInteger(tbUnknownChances.Text);

            saveProfileData.ItemChance = MainWindow.ConvertToInteger(tbItemChance.Text);
            saveProfileData.ItemMinAmount = MainWindow.ConvertToInteger(tbItemMinAmount.Text);
            saveProfileData.ItemMaxAmount = MainWindow.ConvertToInteger(tbItemMaxAmount.Text);
            saveProfileData.ItemTreasureTypeSelectionChances = cbItemTreasureType.SelectedIndex + 1;


            saveProfileData.MagicItemChance = MainWindow.ConvertToInteger(tbMagicItemChance.Text);
            saveProfileData.MagicItemMinAmount = MainWindow.ConvertToInteger(tbMagicItemMinAmount.Text);
            saveProfileData.MagicItemMaxAmount = MainWindow.ConvertToInteger(tbMagicItemMaxAmount.Text);
            saveProfileData.MagicItemTreasureTypeSelectionChances = cbMagicItemTreasureType.SelectedIndex + 1;


            saveProfileData.MundaneItemChance = MainWindow.ConvertToInteger(tbMundaneItemChance.Text);
            saveProfileData.MundaneItemMinAmount = MainWindow.ConvertToInteger(tbMundaneItemMinAmount.Text);
            saveProfileData.MundaneItemMaxAmount = MainWindow.ConvertToInteger(tbMundaneItemMaxAmount.Text);
            saveProfileData.MundaneItemTreasureTypeSelectionChances = cbMundaneItemTreasureType.SelectedIndex + 1;

            saveProfileData.LastModified = string.Format("{0:yyyy-MM-dd hh:mm:ss}", DateTime.Now);


            return saveProfileData;

        }
        public static void SaveDeathTreasureFile(DeathTreasure filedata, string myfilename)
        {
            // Saves sql file

            string header = $"DELETE FROM `treasure_death` WHERE `treasure_Type` = {filedata.DTdid}; \n\n" +
                            "INSERT INTO `treasure_death` (`treasure_Type`, `tier`, `loot_Quality_Mod`, `unknown_Chances`, `item_Chance`, `item_Min_Amount`, `item_Max_Amount`, `item_Treasure_Type_Selection_Chances`, `magic_Item_Chance`, `magic_Item_Min_Amount`, `magic_Item_Max_Amount`, `magic_Item_Treasure_Type_Selection_Chances`, `mundane_Item_Chance`, `mundane_Item_Min_Amount`, `mundane_Item_Max_Amount`, `mundane_Item_Type_Selection_Chances`, `last_Modified`)\n";

            string dateModified = string.Format("'{0:yyyy-MM-dd hh:mm:ss}');", DateTime.Now);

            string dtData = $"VALUES ({filedata.DTdid}, {filedata.Tier}, {filedata.LootQualityMod}, {filedata.UnknownChances}, " +
                            $"{filedata.ItemChance}, {filedata.ItemMinAmount}, {filedata.ItemMaxAmount}, {filedata.ItemTreasureTypeSelectionChances}, " +
                            $"{filedata.MagicItemChance}, {filedata.MagicItemMinAmount}, {filedata.MagicItemMaxAmount}, {filedata.MagicItemTreasureTypeSelectionChances}, " +
                            $"{filedata.MundaneItemChance}, {filedata.MundaneItemMinAmount}, {filedata.MundaneItemMaxAmount}, {filedata.MundaneItemTreasureTypeSelectionChances}," + dateModified;

            File.WriteAllText(myfilename, header + dtData);
        }
        public void CreateLootProfileComboBoxLists()
        {

            //List<string> itemTreasureTableList = new List<string>
            //{"1","2","3","4","5","6","7","8","9","10","11"};

            List<int> itemTreasureTableList = new List<int>
            { 1,2,3,4,5,6,7,8,9,10,11};
            cbItemTreasureType.ItemsSource = itemTreasureTableList;
            cbItemTreasureType.SelectedIndex = 0;

            // List<string> magicItemTreasureTableList = new List<string>();

            List<int> magicItemTreasureTableList = new List<int>
            { 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24};
            cbMagicItemTreasureType.ItemsSource = magicItemTreasureTableList;
            cbMagicItemTreasureType.SelectedIndex = 0;

            //List<string> mundaneItemTreasureTableList = new List<string>();
            List<int> mundaneItemTreasureTableList = new List<int>
            { 1,2,3,4,5,6,7,8};

            cbMundaneItemTreasureType.ItemsSource = mundaneItemTreasureTableList;
            cbMundaneItemTreasureType.SelectedIndex = 0;

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

        private void cbItemTreasureType_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cbItemTreasureType.SelectedIndex + 1 == 1)
            {
                tbItemTreasureChance.Text = $"Loot Type   Percent\n" +
                                            "---------   -------\n" +
                                            "Weapon      100 %\n" +
                                            "Armor       0 %\n" +
                                            "Scroll      0 %\n" +
                                            "Clothing    0 %\n" +
                                            "Jewelry     0 %\n" +
                                            "Gems        0 %\n" +
                                            "ArtObject   0 %\n" +
                                            "PetDevice   0 %";
            }
            else if (cbItemTreasureType.SelectedIndex + 1 == 2)
            {
                tbItemTreasureChance.Text = $"Loot Type   Percent\n" +
                                            "---------   -------\n" +
                                            "Weapon      0 %\n" +
                                            "Armor       100 %\n" +
                                            "Scroll      0 %\n" +
                                            "Clothing    0 %\n" +
                                            "Jewelry     0 %\n" +
                                            "Gems        0 %\n" +
                                            "ArtObject   0 %\n" +
                                            "PetDevice   0 %";
            }
            else if (cbItemTreasureType.SelectedIndex + 1 == 3)
            {
                tbItemTreasureChance.Text = $"Loot Type   Percent\n" +
                                            "---------   -------\n" +
                                            "Weapon      0 %\n" +
                                            "Armor       0 %\n" +
                                            "Scroll      100 %\n" +
                                            "Clothing    0 %\n" +
                                            "Jewelry     0 %\n" +
                                            "Gems        0 %\n" +
                                            "ArtObject   0 %\n" +
                                            "PetDevice   0 %";
            }
            else if (cbItemTreasureType.SelectedIndex + 1 == 4)
            {
                tbItemTreasureChance.Text = $"Loot Type   Percent\n" +
                                            "---------   -------\n" +
                                            "Weapon      0 %\n" +
                                            "Armor       0 %\n" +
                                            "Scroll      0 %\n" +
                                            "Clothing    100 %\n" +
                                            "Jewelry     0 %\n" +
                                            "Gems        0 %\n" +
                                            "ArtObject   0 %\n" +
                                            "PetDevice   0 %";
            }
            else if (cbItemTreasureType.SelectedIndex + 1 == 5)
            {
                tbItemTreasureChance.Text = $"Loot Type   Percent\n" +
                                            "---------   -------\n" +
                                            "Weapon      0 %\n" +
                                            "Armor       0 %\n" +
                                            "Scroll      0 %\n" +
                                            "Clothing    0 %\n" +
                                            "Jewelry     100 %\n" +
                                            "Gems        0 %\n" +
                                            "ArtObject   0 %\n" +
                                            "PetDevice   0 %";
            }
            else if (cbItemTreasureType.SelectedIndex + 1 == 6)
            {
                tbItemTreasureChance.Text = $"Loot Type   Percent\n" +
                                            "---------   -------\n" +
                                            "Weapon      0 %\n" +
                                            "Armor       0 %\n" +
                                            "Scroll      0 %\n" +
                                            "Clothing    0 %\n" +
                                            "Jewelry     0 %\n" +
                                            "Gems        100 %\n" +
                                            "ArtObject   0 %\n" +
                                            "PetDevice   0 %";
            }
            else if (cbItemTreasureType.SelectedIndex + 1 == 7)
            {
                tbItemTreasureChance.Text = $"Loot Type   Percent\n" +
                                            "---------   -------\n" +
                                            "Weapon      0 %\n" +
                                            "Armor       0 %\n" +
                                            "Scroll      0 %\n" +
                                            "Clothing    0 %\n" +
                                            "Jewelry     0 %\n" +
                                            "Gems        0 %\n" +
                                            "ArtObject   100 %\n" +
                                            "PetDevice   0 %";
            }
            else if (cbItemTreasureType.SelectedIndex + 1 == 8)
            {
                tbItemTreasureChance.Text = $"Loot Type   Percent\n" +
                                            "---------   -------\n" +
                                            "Weapon      12.5 %\n" +
                                            "Armor       12.5 %\n" +
                                            "Scroll      12.5 %\n" +
                                            "Clothing    12.5 %\n" +
                                            "Jewelry     12.5 %\n" +
                                            "Gems        12.5 %\n" +
                                            "ArtObject   12.5 %\n" +
                                            "PetDevice   12.5 %";
            }
            else if (cbItemTreasureType.SelectedIndex + 1 == 9)
            {
                tbItemTreasureChance.Text = $"Loot Type   Percent\n" +
                                            "---------   -------\n" +
                                            "Weapon      20 %\n" +
                                            "Armor       20 %\n" +
                                            "Scroll      20 %\n" +
                                            "Clothing    5 %\n" +
                                            "Jewelry     5 %\n" +
                                            "Gems        5 %\n" +
                                            "ArtObject   5 %\n" +
                                            "PetDevice   20 %";
            }
            else if (cbItemTreasureType.SelectedIndex + 1 == 10)
            {
                tbItemTreasureChance.Text = $"Loot Type   Percent\n" +
                                            "---------   -------\n" +
                                            "Weapon      30 %\n" +
                                            "Armor       30 %\n" +
                                            "Scroll      20 %\n" +
                                            "Clothing    0 %\n" +
                                            "Jewelry     0 %\n" +
                                            "Gems        0 %\n" +
                                            "ArtObject   0 %\n" +
                                            "PetDevice   20 %";
            }
            else if (cbItemTreasureType.SelectedIndex + 1 == 11)
            {
                tbItemTreasureChance.Text = $"Loot Type   Percent\n" +
                                            "---------   -------\n" +
                                            "Weapon      0 %\n" +
                                            "Armor       0 %\n" +
                                            "Scroll      0 %\n" +
                                            "Clothing    25 %\n" +
                                            "Jewelry     25 %\n" +
                                            "Gems        25 %\n" +
                                            "ArtObject   25 %\n" +
                                            "PetDevice   0 %";
            }
        }

        private void cbMagicItemTreasureType_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cbMagicItemTreasureType.SelectedIndex + 1 == 1)
            {
                tbMagicItemTreasureChance.Text = $"Loot Type   Percent\n" +
                                            "---------   -------\n" +
                                            "Weapon      100 %\n" +
                                            "Armor       0 %\n" +
                                            "Scroll      0 %\n" +
                                            "Clothing    0 %\n" +
                                            "Jewelry     0 %\n" +
                                            "Gems        0 %\n" +
                                            "ArtObject   0 %\n" +
                                            "Cloak       0 %\n" +
                                            "EncapSpirit 0 %\n";
            }
            else if (cbMagicItemTreasureType.SelectedIndex + 1 == 2)
            {
                tbMagicItemTreasureChance.Text = $"Loot Type   Percent\n" +
                                            "---------   -------\n" +
                                            "Weapon      0 %\n" +
                                            "Armor       100 %\n" +
                                            "Scroll      0 %\n" +
                                            "Clothing    0 %\n" +
                                            "Jewelry     0 %\n" +
                                            "Gems        0 %\n" +
                                            "ArtObject   0 %\n" +
                                            "Cloak       0 %\n" +
                                            "EncapSpirit 0 %\n";
            }
            else if (cbMagicItemTreasureType.SelectedIndex + 1 == 3)
            {
                tbMagicItemTreasureChance.Text = $"Loot Type   Percent\n" +
                                            "---------   -------\n" +
                                            "Weapon      0 %\n" +
                                            "Armor       0 %\n" +
                                            "Scroll      100 %\n" +
                                            "Clothing    0 %\n" +
                                            "Jewelry     0 %\n" +
                                            "Gems        0 %\n" +
                                            "ArtObject   0 %\n" +
                                            "Cloak       0 %\n" +
                                            "EncapSpirit 0 %\n";
            }
            else if (cbMagicItemTreasureType.SelectedIndex + 1 == 4)
            {
                tbMagicItemTreasureChance.Text = $"Loot Type   Percent\n" +
                                            "---------   -------\n" +
                                            "Weapon      0 %\n" +
                                            "Armor       0 %\n" +
                                            "Scroll      0 %\n" +
                                            "Clothing    100 %\n" +
                                            "Jewelry     0 %\n" +
                                            "Gems        0 %\n" +
                                            "ArtObject   0 %\n" +
                                            "Cloak       0 %\n" +
                                            "EncapSpirit 0 %\n";
            }
            else if (cbMagicItemTreasureType.SelectedIndex + 1 == 5)
            {
                tbMagicItemTreasureChance.Text = $"Loot Type   Percent\n" +
                                            "---------   -------\n" +
                                            "Weapon      0 %\n" +
                                            "Armor       0 %\n" +
                                            "Scroll      0 %\n" +
                                            "Clothing    0 %\n" +
                                            "Jewelry     100 %\n" +
                                            "Gems        0 %\n" +
                                            "ArtObject   0 %\n" +
                                            "Cloak       0 %\n" +
                                            "EncapSpirit 0 %\n";
            }
            else if (cbMagicItemTreasureType.SelectedIndex + 1 == 6)
            {
                tbMagicItemTreasureChance.Text = $"Loot Type   Percent\n" +
                                            "---------   -------\n" +
                                            "Weapon      0 %\n" +
                                            "Armor       0 %\n" +
                                            "Scroll      0 %\n" +
                                            "Clothing    0 %\n" +
                                            "Jewelry     0 %\n" +
                                            "Gems        100 %\n" +
                                            "ArtObject   0 %\n" +
                                            "Cloak       0 %\n" +
                                            "EncapSpirit 0 %\n";
            }
            else if (cbMagicItemTreasureType.SelectedIndex + 1 == 7)
            {
                tbMagicItemTreasureChance.Text = $"Loot Type   Percent\n" +
                                            "---------   -------\n" +
                                            "Weapon      0 %\n" +
                                            "Armor       0 %\n" +
                                            "Scroll      0 %\n" +
                                            "Clothing    0 %\n" +
                                            "Jewelry     0 %\n" +
                                            "Gems        0 %\n" +
                                            "ArtObject   100 %\n" +
                                            "Cloak       0 %\n" +
                                            "EncapSpirit 0 %\n";
            }
            else if (cbMagicItemTreasureType.SelectedIndex + 1 == 8)
            {
                tbMagicItemTreasureChance.Text = $"Loot Type   Percent\n" +
                                            "---------   -------\n" +
                                            "Weapon      15 %\n" +
                                            "Armor       15 %\n" +
                                            "Scroll      14 %\n" +
                                            "Clothing    14 %\n" +
                                            "Jewelry     14 %\n" +
                                            "Gems        14 %\n" +
                                            "ArtObject   12.5 %\n" +
                                            "Cloak       1 %\n" +
                                            "EncapSpirit 0.5 %\n";
            }
            else if (cbMagicItemTreasureType.SelectedIndex + 1 == 9)
            {
                tbMagicItemTreasureChance.Text = $"Loot Type   Percent\n" +
                                            "---------   -------\n" +
                                            "Weapon      30 %\n" +
                                            "Armor       30 %\n" +
                                            "Scroll      10 %\n" +
                                            "Clothing    10 %\n" +
                                            "Jewelry     10 %\n" +
                                            "Gems        5 %\n" +
                                            "ArtObject   3.5 %\n" +
                                            "Cloak       1 %\n" +
                                            "EncapSpirit 0.5 %\n";
            }
            else if (cbMagicItemTreasureType.SelectedIndex + 1 == 10)
            {
                tbMagicItemTreasureChance.Text = $"Loot Type   Percent\n" +
                                            "---------   -------\n" +
                                            "Weapon      30 %\n" +
                                            "Armor       30 %\n" +
                                            "Scroll      18.5 %\n" +
                                            "Clothing    10 %\n" +
                                            "Jewelry     10 %\n" +
                                            "Gems        0 %\n" +
                                            "ArtObject   0 %\n" +
                                            "Cloak       1 %\n" +
                                            "EncapSpirit 0.5 %\n";
            }
            else if (cbMagicItemTreasureType.SelectedIndex + 1 == 11)
            {
                tbMagicItemTreasureChance.Text = $"Loot Type   Percent\n" +
                                            "---------   -------\n" +
                                            "Weapon      0 %\n" +
                                            "Armor       0 %\n" +
                                            "Scroll      0 %\n" +
                                            "Clothing    0 %\n" +
                                            "Jewelry     33 %\n" +
                                            "Gems        34 %\n" +
                                            "ArtObject   31.5 %\n" +
                                            "Cloak       1 %\n" +
                                            "EncapSpirit 0.5 %\n";
            }
            else if (cbMagicItemTreasureType.SelectedIndex + 1 == 12)
            {
                tbMagicItemTreasureChance.Text = $"Loot Type   Percent\n" +
                                            "---------   -------\n" +
                                            "Weapon      0 %\n" +
                                            "Armor       0 %\n" +
                                            "Scroll      40 %\n" +
                                            "Clothing    0 %\n" +
                                            "Jewelry     20 %\n" +
                                            "Gems        20 %\n" +
                                            "ArtObject   18.5 %\n" +
                                            "Cloak       1 %\n" +
                                            "EncapSpirit 0.5 %\n";
            }
            else if (cbMagicItemTreasureType.SelectedIndex + 1 == 13)
            {
                tbMagicItemTreasureChance.Text = $"Society Armor        Percent\n" +
                                                  "-------------        -------\n" +
                                                  "SocietyBreastplate   100 %\n";
            }
            else if (cbMagicItemTreasureType.SelectedIndex + 1 == 14)
            {
                tbMagicItemTreasureChance.Text = $"Society Armor        Percent\n" +
                                                  "-------------        -------\n" +
                                                  "SocietyGauntlets     100 %\n";
            }
            else if (cbMagicItemTreasureType.SelectedIndex + 1 == 15)
            {
                tbMagicItemTreasureChance.Text = $"Society Armor        Percent\n" +
                                                  "-------------        -------\n" +
                                                  "SocietyGirth         100 %\n";
            }
            else if (cbMagicItemTreasureType.SelectedIndex + 1 == 16)
            {
                tbMagicItemTreasureChance.Text = $"Society Armor        Percent\n" +
                                                  "-------------        -------\n" +
                                                  "SocietyGreaves       100 %\n";
            }
            else if (cbMagicItemTreasureType.SelectedIndex + 1 == 17)
            {
                tbMagicItemTreasureChance.Text = $"Society Armor        Percent\n" +
                                                  "-------------        -------\n" +
                                                  "SocietyHelm          100 %\n";
            }
            else if (cbMagicItemTreasureType.SelectedIndex + 1 == 18)
            {
                tbMagicItemTreasureChance.Text = $"Society Armor        Percent\n" +
                                                  "-------------        -------\n" +
                                                  "SocietyPauldrons     100 %\n";
            }
            else if (cbMagicItemTreasureType.SelectedIndex + 1 == 19)
            {
                tbMagicItemTreasureChance.Text = $"Society Armor        Percent\n" +
                                                  "-------------        -------\n" +
                                                  "SocietyTassets       100 %\n";
            }
            else if (cbMagicItemTreasureType.SelectedIndex + 1 == 20)
            {
                tbMagicItemTreasureChance.Text = $"Society Armor        Percent\n" +
                                                  "-------------        -------\n" +
                                                  "SocietyVambraces     100 %\n";
            }
            else if (cbMagicItemTreasureType.SelectedIndex + 1 == 21)
            {
                tbMagicItemTreasureChance.Text = $"Society Armor        Percent\n" +
                                                  "-------------        -------\n" +
                                                  "SocietySollerets     100 %\n";
            }
            else if (cbMagicItemTreasureType.SelectedIndex + 1 == 22)
            {
                tbMagicItemTreasureChance.Text = $"Legendary Chest     Percent\n" +
                                                  "---------------     -------\n" +
                                                  "Weapon              35 %\n" +
                                                  "Armor               35 %\n" +
                                                  "Clothing            10 %\n" +
                                                  "Jewelry             10 %\n" +
                                                  "Cloak               5 %\n" +
                                                  "PetDevice           5 %\n";
            }
            else if (cbMagicItemTreasureType.SelectedIndex + 1 == 23)
            {
                tbMagicItemTreasureChance.Text = $"Legendary Magic Chest     Percent\n" +
                                                  "---------------------     -------\n" +
                                                  "Clothing                  30 %\n" +
                                                  "Jewelry                   20 %\n" +
                                                  "Cloak                     20 %\n" +
                                                  "PetDevice                 30 %\n";
            }
            else if (cbMagicItemTreasureType.SelectedIndex + 1 == 24)
            {
                tbMagicItemTreasureChance.Text = $"Mana Forge Chest     Percent\n" +
                                                  "----------------     -------\n" +
                                                  "Weapon               20 %\n" +
                                                  "Armor                20 %\n" +
                                                  "Clothing             30 %\n" +
                                                  "Jewelry              30 %\n";
            }

        }

        private void cbMundaneItemTreasureType_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cbMundaneItemTreasureType.SelectedIndex + 1 == 1)
            {
                tbMundaneItemTreasureChance.Text = $"Loot Type   Percent\n" +
                                            "---------   -------\n" +
                                            "Pyreal      0 %\n" +
                                            "Consumable  100 %\n" +
                                            "HealKit     0 %\n" +
                                            "Lockpick    0 %\n" +
                                            "Spell Comp  0 %\n" +
                                            "Mana Stone  0 %\n ";
            }
            else if (cbMundaneItemTreasureType.SelectedIndex + 1 == 2)
            {
                tbMundaneItemTreasureChance.Text = $"Loot Type   Percent\n" +
                                            "---------   -------\n" +
                                            "Pyreal      0 %\n" +
                                            "Consumable  0 %\n" +
                                            "HealKit     100 %\n" +
                                            "Lockpick    0 %\n" +
                                            "Spell Comp  0 %\n" +
                                            "Mana Stone  0 %\n ";
            }
            else if (cbMundaneItemTreasureType.SelectedIndex + 1 == 3)
            {
                tbMundaneItemTreasureChance.Text = $"Loot Type   Percent\n" +
                                            "---------   -------\n" +
                                            "Pyreal      0 %\n" +
                                            "Consumable  0 %\n" +
                                            "HealKit     0 %\n" +
                                            "Lockpick    100 %\n" +
                                            "Spell Comp  0 %\n" +
                                            "Mana Stone  0 %\n ";
            }
            else if (cbMundaneItemTreasureType.SelectedIndex + 1 == 4)
            {
                tbMundaneItemTreasureChance.Text = $"Loot Type   Percent\n" +
                                            "---------   -------\n" +
                                            "Pyreal      0 %\n" +
                                            "Consumable  0 %\n" +
                                            "HealKit     0 %\n" +
                                            "Lockpick    0 %\n" +
                                            "Spell Comp  100 %\n" +
                                            "Mana Stone  0 %\n ";
            }
            else if (cbMundaneItemTreasureType.SelectedIndex + 1 == 5)
            {
                tbMundaneItemTreasureChance.Text = $"Loot Type   Percent\n" +
                                            "---------   -------\n" +
                                            "Pyreal      0 %\n" +
                                            "Consumable  0 %\n" +
                                            "HealKit     0 %\n" +
                                            "Lockpick    0 %\n" +
                                            "Spell Comp  0 %\n" +
                                            "Mana Stone  100 %\n ";
            }
            else if (cbMundaneItemTreasureType.SelectedIndex + 1 == 6)
            {
                tbMundaneItemTreasureChance.Text = $"Loot Type   Percent\n" +
                                            "---------   -------\n" +
                                            "Pyreal      100 %\n" +
                                            "Consumable  0 %\n" +
                                            "HealKit     0 %\n" +
                                            "Lockpick    0 %\n" +
                                            "Spell Comp  0 %\n" +
                                            "Mana Stone  0 %\n ";
            }
            else if (cbMundaneItemTreasureType.SelectedIndex + 1 == 7)
            {
                tbMundaneItemTreasureChance.Text = $"Loot Type   Percent\n" +
                                            "---------   -------\n" +
                                            "Pyreal      17 %\n" +
                                            "Consumable  17 %\n" +
                                            "HealKit     16 %\n" +
                                            "Lockpick    16 %\n" +
                                            "Spell Comp  17 %\n" +
                                            "Mana Stone  17 %\n ";
            }
            else if (cbMundaneItemTreasureType.SelectedIndex + 1 == 8)
            {
                tbMundaneItemTreasureChance.Text = $"Loot Type   Percent\n" +
                                            "---------   -------\n" +
                                            "Pyreal      34 %\n" +
                                            "Consumable  0 %\n" +
                                            "HealKit     0 %\n" +
                                            "Lockpick    0 %\n" +
                                            "Spell Comp  33 %\n" +
                                            "Mana Stone  33 %\n ";
            }

        }


    }
}
