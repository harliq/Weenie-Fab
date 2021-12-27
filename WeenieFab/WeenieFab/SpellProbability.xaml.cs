using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for SpellProbability.xaml
    /// </summary>
    public partial class SpellProbability : Window
    {
        private DataTable SpellBook;
        private DataTable SpellBookPercent;
        public DataTable SpellBookProbability;


        public SpellProbability(DataTable spellbook)
        {
            InitializeComponent();
            SpellBook = spellbook;
            CalculateSpellPercentTable(SpellBook);
            this.Owner = App.Current.MainWindow;
            //return SpellBookProbability;
            TextBoxSpellBookProb.Visibility = Visibility.Hidden;
        }

        private class SpellBookChances
        {
            public SpellBookChances(int spellID, float spellProbability, string spellName)
            {
                spellId = spellID;
                probability = spellProbability;
                spellname = spellName;
            }
            public int spellId { get; set; }
            public string spellname { get; set; }
            public float probability { get; set; }

        }
        // Button Events
        private void ButtonUpdateSpellPercent_Click(object sender, RoutedEventArgs e)
        {
            var index = dgSpellProbability.SelectedIndex;
            if (index < 0)
            {
                MessageBox.Show("Please Select a row");
                return;
            }
            try
            {
                DataGridRow currentRowIndex = dgSpellProbability.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
                DataRow dr = SpellBookPercent.Rows[currentRowIndex.GetIndex()];

                dr[0] = MainWindow.ConvertToInteger(tbSpellId.Text);
                dr[1] = MainWindow.ConvertToFloat(tbSpellPercentValue.Text);
                dr[2] = tbSpellDescription.Text;

                SpellBookPercent.AcceptChanges();
                dgSpellProbability.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("The row you selected is blank");
                MainWindow.LogError(ex);
            }
            CalculateTotalPercentChance(SpellBookPercent, false);

        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
        private void ButtonGenerateProbSpellBookTable_Click(object sender, RoutedEventArgs e)
        {
            float testChance = CalculateTotalPercentChance(SpellBookPercent, false);
            if(testChance > 100)
            {
                MessageBox.Show("Warning! Total Percent is over 100%! Please fix first!");
                return;
            }

            var percentList = GetDataTableProbability(SpellBookPercent, false);
            var probabilityList = ConvertSpellPercentToSpellBookProbability(percentList);
            TextBoxSpellBookProb.Text = "";

            foreach (var spell in probabilityList)
            {
                
                TextBoxSpellBookProb.Text += $"{spell}\n";
            }
            CreateProbabilityTable(SpellBookPercent);

            this.DialogResult = true;
            this.Close();
        }
        private void CalculateSpellPercentTable(DataTable weenieSpellBook)
        {
            var spellBookList = ConvertDataTableToList(weenieSpellBook);
            var probabilityList = GetDataTableProbability(weenieSpellBook, true);
            var totalChance = GetProbabilityAny(probabilityList);

            SpellBookPercent = SpellbookToIndependent(probabilityList, spellBookList);
            dgSpellProbability.DataContext = SpellBookPercent;

            if (totalChance > 1)
                LabelTotalSpellPercent.Foreground = Brushes.Red;
            else
                LabelTotalSpellPercent.Foreground = Brushes.Green;
            LabelTotalSpellPercent.Content = PercentFormat(totalChance) + "%";

        }
        private DataTable SpellbookToIndependent(List<float> SpellProb, List<SpellBookChances> spellProbabilities)
        {
            DataTable tempDataTable = MainWindow.spellDataTable.Clone();
            tempDataTable.Clear();

            for (var i = 0; i < SpellProb.Count; i++)
            {
                var prevChanceNone = i > 0 ? GetProbabilityNone(SpellProb.GetRange(0, i)) : 1.0f;
                DataRow dr = tempDataTable.NewRow();

                dr[0] = spellProbabilities.ElementAt(i).spellId;
                dr[1] = PercentFormat(SpellProb[i] * prevChanceNone);
                dr[2] = spellProbabilities.ElementAt(i).spellname;
                tempDataTable.Rows.Add(dr);
            }
            return tempDataTable;
        }

        private List<SpellBookChances> ConvertDataTableToList(DataTable dataTable)
        {
            List<SpellBookChances> spellProbList = new List<SpellBookChances>();
            int rowcount = dataTable.Rows.Count;
            float tempProb = 0;

            if (rowcount > 0)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    tempProb = MainWindow.ConvertToFloat(row[1].ToString());
                    spellProbList.Add(new SpellBookChances(MainWindow.ConvertToInteger(row[0].ToString()), MainWindow.ConvertToFloat(row[1].ToString()) - 2, row[2].ToString()));
                }
            }
            return spellProbList;
        }
        private List<float> GetDataTableProbability(DataTable dataTable, bool subtractTwo)
        {
            List<float> spellProb = new List<float>();
            int rowcount = dataTable.Rows.Count;
            float tempProb = 0;

            foreach (DataRow row in dataTable.Rows)
            {
                tempProb = MainWindow.ConvertToFloat(row[1].ToString());
                if(subtractTwo)
                    spellProb.Add(MainWindow.ConvertToFloat(row[1].ToString()) - 2);
                else
                    spellProb.Add(MainWindow.ConvertToFloat(row[1].ToString()));
            }
            return spellProb;
        }
        public static float GetProbabilityAny(List<float> chances)
        {
            return 1.0f - GetProbabilityNone(chances);
        }
        public static float GetProbabilityNone(List<float> chances)
        {
            var probability = 1.0f;

            foreach (var chance in chances)
                probability *= 1.0f - chance;

            return probability;
        }
        public static string PercentFormat(float percent)
        {
            return $"{Math.Round(percent * 100, 2)}";
        }
        private List<float> ConvertSpellPercentToSpellBookProbability(List<float> percentChances)
        {
            var chances = new List<float>();

            foreach(var spellpercentchance in percentChances)
            {              
                chances.Add(spellpercentchance / 100);
            }
            var spellbook = new List<float>();
            for (var i = 0; i < chances.Count; i++)
            {
                var prevChanceNone = i > 0 ? GetProbabilityNone(spellbook) : 1.0f;               
                spellbook.Add(chances[i] / prevChanceNone);                
            }
            var spellbookFinal = new List<float>();
            foreach(var tempProb in spellbook)
            {
                spellbookFinal.Add((float)Math.Round(tempProb + 2, 2));
            }

            return spellbookFinal;
        }

        private void dgSpellProbability_Selected(object sender, RoutedEventArgs e)
        {
            var index = dgSpellProbability.SelectedIndex;
            DataGridRow currentRowIndex = dgSpellProbability.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
            if (index + 1 > SpellBookPercent.Rows.Count)
            {
            }
            else
            {
                DataRow dr = SpellBookPercent.Rows[currentRowIndex.GetIndex()];
                tbSpellId.Text = dr[0].ToString();
                tbSpellPercentValue.Text = dr[1].ToString();
                tbSpellDescription.Text = dr[2].ToString();
            }
        }
        private float CalculateTotalPercentChance(DataTable dataTable, bool removeTwo)
        {
            float totalPercentChance = 0;

            foreach (DataRow row in dataTable.Rows)
            {
                totalPercentChance += MainWindow.ConvertToFloat(row[1].ToString());
            }
            if (totalPercentChance > 100)
                LabelTotalSpellPercent.Foreground = Brushes.Red;
            else
                LabelTotalSpellPercent.Foreground = Brushes.Green;
            LabelTotalSpellPercent.Content = totalPercentChance + "%";
            return totalPercentChance;
        }
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            // for .NET Core you need to add UseShellExecute = true
            // see https://docs.microsoft.com/dotnet/api/system.diagnostics.processstartinfo.useshellexecute#property-value
            Process browser = new Process();
            browser.StartInfo.UseShellExecute = true;
            browser.StartInfo.FileName = e.Uri.AbsoluteUri;
            browser.Start();
            e.Handled = true;
        }
        private void CreateProbabilityTable(DataTable dataTable)
        {
            var percentList = GetDataTableProbability(dataTable, false);
            var probabilityList = ConvertSpellPercentToSpellBookProbability(percentList);
            var spellBookList = ConvertDataTableToList(dataTable);

            DataTable tempDataTable = MainWindow.spellDataTable.Clone();
            tempDataTable.Clear();

            for (var i = 0; i < probabilityList.Count; i++)
            {
                DataRow dr = tempDataTable.NewRow();
                float tempProb = probabilityList[i];

                dr[0] = spellBookList.ElementAt(i).spellId;
                dr[1] = tempProb;
                dr[2] = spellBookList.ElementAt(i).spellname;
                tempDataTable.Rows.Add(dr);
            }
            SpellBookProbability = tempDataTable;
        }
        private void FloatValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.-]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
