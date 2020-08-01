using Microsoft.Win32;
using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WeenieFab.Properties;

namespace WeenieFab
{
    /// <summary>
    /// Interaction logic for Converter.xaml
    /// </summary>
    public partial class Converter : Window
    {
        public Converter()
        {
            InitializeComponent();
            tbConvertFilePath.Text = WeenieFabUser.Default.DefaultSqlPath;
            tbConvertSqlFilePath.Text = WeenieFabUser.Default.DefaultJsonPath;
        }

        private void btnJsonFiles_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Title = "Select JSON Files to Convert";
            ofd.Filter = "JSON files|*.json";
            ofd.InitialDirectory = WeenieFabUser.Default.DefaultJsonPath;
            Nullable<bool> result = ofd.ShowDialog();

            if (result == true)
            {
                foreach (String filename in ofd.FileNames)
                {
                    tbJsonFiles.Text += filename + "\r\n";
                }

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            string jsonConvertFiles = tbJsonFiles.Text;
            string[] tempJsonFiles = jsonConvertFiles.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            int c = 0;
            foreach (var fileLine in tempJsonFiles)
            {
                c++;

                FileInfo jfileinfo = new FileInfo(fileLine);
                DirectoryInfo directoryInfo = new DirectoryInfo(tbConvertFilePath.Text);

                try
                {
                    ACDataLib.Converter.json2sql(jfileinfo, null, directoryInfo);
                }
                catch (Exception ex)
                {
                    MainWindow.LogError(ex);
                    MessageBoxButton buttons = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.Error;
                    MessageBoxResult result = MessageBox.Show("Issue with File. Please send WeenieFabErrorLog.txt to Harli Quinn on Discord", "ERROR!", buttons, icon);

                }
                
            }
            MessageBox.Show($"{c} files were converted.");
        }

        private void btnChangeFolder_Click(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog fbd = new VistaFolderBrowserDialog();
            fbd.Description = "Please select folder for Converted JSON Files.";
            fbd.UseDescriptionForTitle = true; // This applies to the Vista style dialog only, not the old dialog.
            if (!VistaFolderBrowserDialog.IsVistaFolderDialogSupported)
                MessageBox.Show(this, "Because you are not using Windows Vista or later, the regular folder browser dialog will be used. Please use Windows Vista to see the new dialog.", "Sample folder browser dialog");
            if ((bool)fbd.ShowDialog(this))
            {
                tbConvertFilePath.Text = fbd.SelectedPath;
            }
        }

        // SQL -> JSON Converter Tab
        private void btnSQLFiles_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Title = "Select SQL Files to Convert";
            ofd.Filter = "SQL files|*.sql";
            ofd.InitialDirectory = WeenieFabUser.Default.DefaultSqlPath;
            Nullable<bool> result = ofd.ShowDialog();

            if (result == true)
            {
                foreach (String filename in ofd.FileNames)
                {
                    tbSqlFiles.Text += filename + "\r\n";
                }

            }
        }

        private void btnChangeJSONFolder_Click(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog fbd = new VistaFolderBrowserDialog();
            fbd.Description = "Please select folder for Converted SQL Files.";
            fbd.UseDescriptionForTitle = true; // This applies to the Vista style dialog only, not the old dialog.
            if (!VistaFolderBrowserDialog.IsVistaFolderDialogSupported)
                MessageBox.Show(this, "Because you are not using Windows Vista or later, the regular folder browser dialog will be used. Please use Windows Vista to see the new dialog.", "Sample folder browser dialog");
            if ((bool)fbd.ShowDialog(this))
            {
                tbConvertSqlFilePath.Text = fbd.SelectedPath;
            }
        }

        private void btnConvertSqlFiles_Click(object sender, RoutedEventArgs e)
        {

            string sqlConvertFiles = tbSqlFiles.Text;
            string[] tempSqlFiles = sqlConvertFiles.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            int c = 0;
            foreach (var fileLine in tempSqlFiles)
            {
                c++;

                FileInfo sqlfileinfo = new FileInfo(fileLine);
                DirectoryInfo directoryInfo = new DirectoryInfo(tbConvertSqlFilePath.Text);

                try
                {
                    ACDataLib.Converter.sql2json(sqlfileinfo, null, directoryInfo);
                }
                catch (Exception ex)
                {
                    MainWindow.LogError(ex);
                    MessageBoxButton buttons = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.Error;
                    MessageBoxResult result = MessageBox.Show("Issue with File. Please send WeenieFabErrorLog.txt to Harli Quinn on Discord", "ERROR!", buttons, icon);

                }

            }
            MessageBox.Show($"{c} files were converted.");

        }
        private void btnCloseConverter_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
