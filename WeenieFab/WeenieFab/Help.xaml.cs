using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
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
    /// Interaction logic for Help.xaml
    /// </summary>
    public partial class Help : Window
    {
        public Help()
        {
            InitializeComponent();
            CreateResourceList();
            
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public void CreateResourceList()
        {
            List<HelpResources> listResources = new List<HelpResources>();

            foreach (string line in File.ReadLines(@"TypeLists\HelpResources.txt"))
            {
                string[] rData = line.Split(",");

                listResources.Add(new HelpResources { Website = rData[0], Address = rData[1] });
            }
            lvResources.ItemsSource = listResources;
        }       
        private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            var urlPart = ((Hyperlink)sender).NavigateUri;
            var fullUrl = urlPart.ToString();
            // System.Diagnostics.Process.Start(new ProcessStartInfo("gooogle.com"));
            // Process.Start(new ProcessStartInfo(fullUrl));
            Process.Start(new ProcessStartInfo("cmd", $"/c start {fullUrl}")); 
            // Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
           
            try
            {
                Process.Start(new ProcessStartInfo("cmd", $"/c start {fullUrl}"));
            }
            catch (Exception)
            {

                throw;
            }
            e.Handled = true;

        }

    }
}
