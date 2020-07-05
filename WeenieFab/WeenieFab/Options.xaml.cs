using System.Windows;
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
