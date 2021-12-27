using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WeenieFab
{
    /// <summary>
    /// Interaction logic for SpellProbability.xaml
    /// </summary>
    public partial class SpellProbability : Window
    {
        private DataTable SpellBook;

        public SpellProbability(DataTable spellbook)
        {
            InitializeComponent();
            SpellBook = spellbook;
            CalculateSpellProbTable(SpellBook);
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
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void CalculateSpellProbTable(DataTable weenieSpellBook)
        {
            var spellBookList = ConvertDataTableToList(weenieSpellBook);
            var probabilityList = GetDataTableProbability(weenieSpellBook);
            var totalChance = GetProbabilityAny(probabilityList);

            dgSpellProbability.DataContext = SpellbookToIndependent(probabilityList, spellBookList);
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
        private List<float> GetDataTableProbability(DataTable dataTable)
        {
            List<float> spellProb = new List<float>();
            int rowcount = dataTable.Rows.Count;
            float tempProb = 0;

            foreach (DataRow row in dataTable.Rows)
            {
                tempProb = MainWindow.ConvertToFloat(row[1].ToString());
                spellProb.Add(MainWindow.ConvertToFloat(row[1].ToString()) - 2);
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
    }
}
