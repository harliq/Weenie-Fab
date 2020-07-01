using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WeenieFab
{
    public partial class MainWindow : Window
    {
        // Button Events
        private void btnAddInt32_Click(object sender, RoutedEventArgs e)
        {
            // search for duplicate property
            if (SearchForDuplicateProps(integerDataTable, cbInt32Props.SelectedIndex.ToString()))
            {
                MessageBox.Show("Property Already Exits");
            }
            else
            {
                string[] description = cbInt32Props.Text.Split(" ");
                DataRow dr = integerDataTable.NewRow();
                dr[0] = cbInt32Props.SelectedIndex.ToString();
                dr[1] = tbValue.Text;
                dr[2] = description[1];

                integerDataTable.Rows.Add(dr);
            }
        }           
        private void btnInt32Remove_Click(object sender, RoutedEventArgs e)
        {
            var index = dgInt32.SelectedIndex;
            try
            {
                DataGridRow currentRowIndex = dgInt32.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
                DataRow dr = integerDataTable.Rows[currentRowIndex.GetIndex()];
                dr.Delete();
                integerDataTable.AcceptChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("You can not delete that row!");
            }
        }
        private void btnUpdateInt32_Click_1(object sender, RoutedEventArgs e)
        {
            var index = dgInt32.SelectedIndex;
            if (index < 0)
            {
                MessageBox.Show("Please Select a row");
                return;
            }
            try
            {
                DataGridRow currentRowIndex = dgInt32.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
                DataRow dr = integerDataTable.Rows[currentRowIndex.GetIndex()];

                string[] description = cbInt32Props.Text.Split(" ");

                dr[0] = cbInt32Props.SelectedIndex.ToString();
                dr[1] = tbValue.Text;
                dr[2] = description[1];

                integerDataTable.AcceptChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("The row you selected is blank");
            }
        }
        private void btnAddInt64_Click(object sender, RoutedEventArgs e)
        {
            // search for duplicate property
            if (SearchForDuplicateProps(integer64DataTable, cbInt64Props.SelectedIndex.ToString()))
            {
                MessageBox.Show("Property Already Exits");
            }
            else
            {
                string[] description = cbInt64Props.Text.Split(" ");
                DataRow dr = integer64DataTable.NewRow();
                dr[0] = cbInt64Props.SelectedIndex.ToString();
                dr[1] = tb64Value.Text;
                dr[2] = description[1];

                integer64DataTable.Rows.Add(dr);
            }
        }
        private void btnRemoveInt64_Click(object sender, RoutedEventArgs e)
        {
            var index = dgInt64.SelectedIndex;
            try
            {
                DataGridRow currentRowIndex = dgInt64.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
                DataRow dr = integer64DataTable.Rows[currentRowIndex.GetIndex()];
                dr.Delete();
                integer64DataTable.AcceptChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("You can not delete that row!");
            }
        }
        private void btnUpdateInt64_Click_1(object sender, RoutedEventArgs e)
        {
            var index = dgInt64.SelectedIndex;
            if (index < 0)
            {
                MessageBox.Show("Please Select a row");
                return;
            }
            try
            {
                DataGridRow currentRowIndex = dgInt64.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
                DataRow dr = integer64DataTable.Rows[currentRowIndex.GetIndex()];

                string[] description = cbInt64Props.Text.Split(" ");

                dr[0] = cbInt64Props.SelectedIndex.ToString();
                dr[1] = tb64Value.Text;
                dr[2] = description[1];

                integer64DataTable.AcceptChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("The row you selected is blank");
            }

        }
        
    }
}
