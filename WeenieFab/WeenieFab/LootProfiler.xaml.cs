using System;
using System.Windows;
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

            }
        }
    }
}
