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
using System.IO;
using System.Drawing;
using WeenieFab.Properties;

namespace WeenieFab
{
    /// <summary>
    /// Interaction logic for Options.xaml
    /// </summary>
    public partial class Options : Window
    {

        public Options()
        {
            InitializeComponent();
            LoadPaths();
        }

        private void LoadPaths()
        {
            tbDefaultSqlPath.Text = WeenieFabUser.Default.DefaultSqlPath;
            tbDefaultJsonPath.Text = WeenieFabUser.Default.DefaultJsonPath;
            btnSetSqlPath.Visibility = Visibility.Hidden;
            btnSetJsonPath.Visibility = Visibility.Hidden;
        }

        private void btnSetSqlPath_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Properties.WeenieFabUser.Default.DefaultSqlPath = tbDefaultSqlPath.Text;
            Properties.WeenieFabUser.Default.DefaultJsonPath = tbDefaultJsonPath.Text;
            WeenieFabUser.Default.Save();

            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}
