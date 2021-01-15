using Ookii.Dialogs.Wpf;
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
            tbDefaultESPath.Text = WeenieFabUser.Default.DefaultESPath;
            chkbAutoLoadEsFiles.IsChecked = WeenieFabUser.Default.AutoLoadESFiles;
            chkbUseLastPathFileOpenSave.IsChecked = WeenieFabUser.Default.UseFilePaths;
        }
        private void btnSetSqlPath_Click(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog fbd = new VistaFolderBrowserDialog();
            fbd.Description = "Please select a default SQL folder.";
            fbd.UseDescriptionForTitle = true; // This applies to the Vista style dialog only, not the old dialog.
            if (!VistaFolderBrowserDialog.IsVistaFolderDialogSupported)
                MessageBox.Show(this, "Because you are not using Windows Vista or later, the regular folder browser dialog will be used. Please use Windows Vista to see the new dialog.", "Sample folder browser dialog");
            if ((bool)fbd.ShowDialog(this))
            {
                tbDefaultSqlPath.Text= fbd.SelectedPath;
            }
        }
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Properties.WeenieFabUser.Default.DefaultSqlPath = tbDefaultSqlPath.Text;
            Properties.WeenieFabUser.Default.DefaultJsonPath = tbDefaultJsonPath.Text;
            Properties.WeenieFabUser.Default.DefaultESPath = tbDefaultESPath.Text;
            Properties.WeenieFabUser.Default.AutoLoadESFiles = chkbAutoLoadEsFiles.IsChecked.Value;
            Properties.WeenieFabUser.Default.UseFilePaths = chkbUseLastPathFileOpenSave.IsChecked.Value;

            WeenieFabUser.Default.Save();

            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnSetJsonPath_Click(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog fbd = new VistaFolderBrowserDialog();
            fbd.Description = "Please select a default JSON folder.";
            fbd.UseDescriptionForTitle = true; // This applies to the Vista style dialog only, not the old dialog.
            if (!VistaFolderBrowserDialog.IsVistaFolderDialogSupported)
                MessageBox.Show(this, "Because you are not using Windows Vista or later, the regular folder browser dialog will be used. Please use Windows Vista to see the new dialog.", "Sample folder browser dialog");
            if ((bool)fbd.ShowDialog(this))
            {
                tbDefaultJsonPath.Text = fbd.SelectedPath;
            }
        }
        private void btnSetESPath_Click(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog fbd = new VistaFolderBrowserDialog();
            fbd.Description = "Please select a default EmoteScript folder.";
            fbd.UseDescriptionForTitle = true; // This applies to the Vista style dialog only, not the old dialog.
            if (!VistaFolderBrowserDialog.IsVistaFolderDialogSupported)
                MessageBox.Show(this, "Because you are not using Windows Vista or later, the regular folder browser dialog will be used. Please use Windows Vista to see the new dialog.", "Sample folder browser dialog");
            if ((bool)fbd.ShowDialog(this))
            {
                tbDefaultESPath.Text = fbd.SelectedPath;
            }
        }
    }
}
