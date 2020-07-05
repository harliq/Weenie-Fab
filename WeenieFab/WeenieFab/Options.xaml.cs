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


            //FolderBrowserDialog fbd = new FolderBrowserDialog();
            
            //    System.Windows.Forms.DialogResult result = fbd.ShowDialog();
                    
            //    if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            //    {
            //        // Options.
                
            //        //string[] files = Directory.GetFiles(fbd.SelectedPath);

            //        //System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
            //    }
            
               // Options

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


        //private FolderBrowserDialog folderBrowserDialog1;
        //private OpenFileDialog openFileDialog1;

        ////private RichTextBox richTextBox1;

        ////private MainMenu mainMenu1;
        //private MenuItem fileMenuItem, openMenuItem;
        //private MenuItem folderMenuItem, closeMenuItem;

        //private string openFileName, folderName;

        //private bool fileOpened = false;













        //// Bring up a dialog to open a file.
        //private void openMenuItem_Click(object sender, System.EventArgs e)
        //{
        //    // If a file is not opened, then set the initial directory to the
        //    // FolderBrowserDialog.SelectedPath value.
        //    if (!fileOpened)
        //    {
        //        openFileDialog1.InitialDirectory = folderBrowserDialog1.SelectedPath;
        //        openFileDialog1.FileName = null;
        //    }

        //    // Display the openFile dialog.
        //    bool? result = openFileDialog1.ShowDialog()
        //    if (result = true)
        //    {
        //        openFileName = openFileDialog1.FileName;
        //        try
        //        {

        //        }
        //        catch (Exception exp)
        //        {
        //            System.Windows.MessageBox.Show("An error occurred while attempting to load the file. The error is:"
        //                            + System.Environment.NewLine + exp.ToString() + System.Environment.NewLine);
        //            fileOpened = false;
        //        }

        //    }

        //    // Cancel button was pressed.
        //    else if (result == DialogResult.Cancel)
        //    {
        //        return;
        //    }
        //}

















        // FolderBrowserDialog fbd = new FolderBrowserDialog();

        // if (fbd.ShowDialog.)



        //using (var fbd = new FolderBrowserDialog())
        //{
        //    DialodigResult result = fbd.ShowDialog();

        //    if (result == DialogResult. && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
        //    {
        //        string[] files = Directory.GetFiles(fbd.SelectedPath);

        //        System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
        //    }
        //}
    }



}
