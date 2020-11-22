using System;
using System.Collections.Generic;
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
    }
}
